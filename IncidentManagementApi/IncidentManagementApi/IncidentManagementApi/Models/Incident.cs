using IncidentManagementApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace IncidentManagementApi.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public Severity Severity { get; set; } = Severity.Medium;
        public Status Status { get; set; } = Status.Open;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public List<Attachment> Attachments { get; set; } = new();
    }
}
