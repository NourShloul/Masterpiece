using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class ServiceRequest
{
    public int RequestId { get; set; }

    public string ContactName { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public string? SubServices { get; set; }

    public string ProjectDescription { get; set; } = null!;

    public string ProjectDuration { get; set; } = null!;

    public decimal ExpectedBudget { get; set; }

    public string MeetingPreference { get; set; } = null!;

    public string RequestStatus { get; set; } = null!;

    public DateTime? RequestDate { get; set; }
}
