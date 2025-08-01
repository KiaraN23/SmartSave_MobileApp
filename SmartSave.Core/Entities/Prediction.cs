using SmartSave.Core.Common;

namespace SmartSave.Core.Entities
{
    public class Prediction : TimestampedEntity
    {
        public int UserId { get; set; }
        public string PredictionMessage { get; set; }
    }
}
