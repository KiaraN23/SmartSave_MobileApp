using SmartSave.Core.Enums;
using System.Text.Json.Serialization;

namespace SmartSave.Application.DTOs
{
    public class GetTransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
    }
}
