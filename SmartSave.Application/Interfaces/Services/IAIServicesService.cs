using SmartSave.Application.DTOs;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IAIServicesService
    {
        public Task SaveSuggestionAsync(GetSuggestionDto getSuggestionDto, DateTime dateTime, int userId);
        public Task SavePredictionAsync(GetPredictionDto getPredictionDto, DateTime dateTime, int userId);
    }
}
