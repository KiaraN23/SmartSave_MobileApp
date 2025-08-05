using System.Text.Json.Serialization;

namespace SmartSave.Application.DTOs
{
    public class CreateDebtDto
    {
        public string Creditor { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
