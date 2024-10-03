using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public int? OrderId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? TransactionId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
