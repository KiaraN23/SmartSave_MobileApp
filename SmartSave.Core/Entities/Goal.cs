using SmartSave.Core.Common;

namespace SmartSave.Core.Entities
{
    public class Goal : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ObjectiveAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
