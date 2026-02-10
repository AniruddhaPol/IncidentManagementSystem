using IncidentManagementApi.Data;
using IncidentManagementApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*IF we need to use AZURE KEY VAULT : we can use below implementation
 
 builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite(builder.Configuration["SqliteConnectionString"]));

var azureStorageConnectionString = builder.Configuration["AzureStorage--ConnectionString"];

//Blob & Queue service registration(pull from config)
builder.Services.AddSingleton(sp =>
{
    var container = builder.Configuration["AzureStorage--ContainerName"] ?? "incident-attachments";
    return new BlobService(azureStorageConnectionString, container);
});

builder.Services.AddSingleton(sp =>
{
    var queueName = builder.Configuration["AzureStorage--QueueName"] ?? "incident-notifications";
    return new QueueService(azureStorageConnectionString, queueName);
});
 
 */

// config
var config = builder.Configuration;

// Add DbContext
// SQLite config

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite("Data Source=incidents.db"));

//Blob & Queue service registration(pull from config)
builder.Services.AddSingleton(sp =>
{
    var cs = config["AzureStorage:ConnectionString"];
    var container = config["AzureStorage:ContainerName"] ?? "incident-attachments";
    return new BlobService(cs, container);
});

builder.Services.AddSingleton(sp =>
{
    var cs = config["AzureStorage:ConnectionString"];
    var queueName = config["AzureStorage:QueueName"] ?? "incident-notifications";
    return new QueueService(cs, queueName);
});


builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("https://incident-react-app-efadh9fgfdatdahv.eastus-01.azurewebsites.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbPath = Path.Combine(app.Services.GetRequiredService<IWebHostEnvironment>().ContentRootPath, "incidents.db");
if (File.Exists(dbPath))
{
    File.Delete(dbPath);   // remove old broken schema
}
// Ensure DB schema is created for SQLite
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment() || true)
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();