using SmartSave.Core.Common;

namespace SmartSave.Core.Entities
{
    public class Suggestion : BaseEntity
    {
        public int UserId { get; set; }
        public string SuggestionMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
