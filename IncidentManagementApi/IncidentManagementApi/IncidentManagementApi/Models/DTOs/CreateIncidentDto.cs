namespace IncidentManagementApi.Models.DTOs
{
    public record CreateIncidentDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Severity { get; set; } = "";
        public IFormFile File { get; set; }
    }
}
