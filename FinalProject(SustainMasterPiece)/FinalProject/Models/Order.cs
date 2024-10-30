using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Message { get; set; } = null!;

    public virtual ICollection<FormCheckboxChoice> FormCheckboxChoices { get; set; } = new List<FormCheckboxChoice>();

    public virtual User? User { get; set; }
}
