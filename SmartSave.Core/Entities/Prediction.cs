using SmartSave.Core.Common;

namespace SmartSave.Core.Entities
{
    public class Prediction : TimestampedEntity
    {
        public string PredictionMessage { get; set; }
    }
}
