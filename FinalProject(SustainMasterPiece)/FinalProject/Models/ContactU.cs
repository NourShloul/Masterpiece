using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class ContactU
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Message { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
