using IncidentManagementApi.Data;
using IncidentManagementApi.Enums;
using IncidentManagementApi.Models;
using IncidentManagementApi.Models.DTOs;
using IncidentManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IncidentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly BlobService _blobService;
        private readonly QueueService _queueService;
        private readonly IWebHostEnvironment _environment;


        public IncidentsController(AppDbContext appContext, BlobService blobService, QueueService queueService, IWebHostEnvironment environment)
        {
            _db = appContext;
            _blobService = blobService;
            _queueService = queueService;
            _environment = environment;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 50)
        {
            var items = await _db.Incidents
                                 .Include(i => i.Attachments)
                                 .OrderByDescending(i => i.CreatedAt)
                                 .Skip((page - 1) * size)
                                 .Take(size)
                                 .ToListAsync();
            return Ok(items);
        }      
       

        // POST api/<ValuesController>
        [HttpPost]
        [RequestSizeLimit(50_000_000)] // allow up to 50MB
        public async Task<IActionResult> Create([FromForm] CreateIncidentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var inc = new Incident
            {
                Title = dto.Title,
                Description = dto.Description,
                Severity = (Severity)Enum.Parse(typeof(Severity), dto.Severity),
                Status = Status.Open
            };

            _db.Incidents.Add(inc);
             await _db.SaveChangesAsync(); 
            
            var incidentId = inc.Id; // get inc.Id
            if (dto.File != null && dto.File.Length > 0)
            {
                var blobName = $"incidents/{incidentId}/{Guid.NewGuid()}_{Path.GetFileName(dto.File.FileName)}";
                using var stream = dto.File.OpenReadStream();
                var blobUrl = await _blobService.UploadAsync(stream, blobName, dto.File.ContentType);

                var att = new Attachment { IncidentId = incidentId, BlobUrl = blobUrl, FileName = dto.File.FileName, Size = dto.File.Length };
                _db.Attachments.Add(att);
                await _db.SaveChangesAsync();
            }

            // enqueue for notification
            await _queueService.EnqueueAsync(new { incidentId, inc.Title, inc.Severity, CreatedAt = inc.CreatedAt });

            var created = await _db.Incidents.Include(i => i.Attachments).FirstAsync(i => i.Id == incidentId);
            return CreatedAtAction(nameof(GetById), new { id = incidentId }, created);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inc = await _db.Incidents.Include(i => i.Attachments).FirstOrDefaultAsync(i => i.Id == id);
            if (inc == null) return NotFound();
            return Ok(inc);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
        {
            var inc = await _db.Incidents.FindAsync(id);
            if (inc == null) return NotFound();

            inc.Status = (Status)Enum.Parse(typeof(Status), dto.Status);
            inc.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(inc);
        }

    }
}
