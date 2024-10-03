namespace FinalProject.DTO
{
    public class PaymentDTO
    {
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TransactionId { get; set; }
    }
}
