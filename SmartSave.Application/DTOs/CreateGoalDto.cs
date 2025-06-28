using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartSave.Application.DTOs
{
    public class CreateGoalDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal ObjectiveAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
