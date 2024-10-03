using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class SubService
{
    public int Id { get; set; }

    public int? ServiceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Service? Service { get; set; }
}
