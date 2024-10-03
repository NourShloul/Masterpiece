using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<SubService> SubServices { get; set; } = new List<SubService>();
}
