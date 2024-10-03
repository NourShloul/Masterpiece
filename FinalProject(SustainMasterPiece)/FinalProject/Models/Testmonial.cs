using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Testmonial
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public string? Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
