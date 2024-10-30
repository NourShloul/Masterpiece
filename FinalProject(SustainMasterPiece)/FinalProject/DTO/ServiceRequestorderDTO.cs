namespace FinalProject.DTO
{
    public class ServiceRequestorderDTO
    {
        public string ContactName { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string ContactPhone { get; set; } = null!;

        public string ServiceType { get; set; } = null!;

        public string? SubServices { get; set; }

        public string ProjectDescription { get; set; } = null!;

        public string ProjectDuration { get; set; } = null!;

        public decimal ExpectedBudget { get; set; }

        public string MeetingPreference { get; set; } = null!;

    }
}
