using System.Text.Json.Serialization;

namespace SmartSave.Application.DTOs
{
    public class BasicResponse
    {
        [JsonIgnore]
        public bool HasError { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public string? ErrorMessage { get; set; }
    }
}
