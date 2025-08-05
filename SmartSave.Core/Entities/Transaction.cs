using SmartSave.Core.Common;
using SmartSave.Core.Enums;

namespace SmartSave.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
    }
}
