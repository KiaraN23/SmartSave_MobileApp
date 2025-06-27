using SmartSave.Core.Enums;
using System.Text.Json.Serialization;

namespace SmartSave.Application.DTOs
{
    public class CreateTransactionDto
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
    }
}
