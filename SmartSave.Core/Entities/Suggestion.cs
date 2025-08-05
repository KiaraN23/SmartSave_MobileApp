using SmartSave.Core.Common;

namespace SmartSave.Core.Entities
{
    public class Suggestion : TimestampedEntity
    {
        public string SuggestionMessage { get; set; }
    }
}
