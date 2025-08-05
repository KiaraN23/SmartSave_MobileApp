namespace SmartSave.Application.DTOs
{
    public class GetDebtDto
    {
        public int Id { get; set; }
        public string Creditor { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
