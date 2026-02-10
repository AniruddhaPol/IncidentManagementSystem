using IncidentManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>()
             .HasOne(a => a.Incident)
             .WithMany(i => i.Attachments)
             .HasForeignKey(a => a.IncidentId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
