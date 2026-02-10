using System.Text.Json.Serialization;

namespace IncidentManagementApi.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string BlobUrl { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int IncidentId { get; set; }
        [JsonIgnore]
        public Incident Incident { get; set; }
    }
}
