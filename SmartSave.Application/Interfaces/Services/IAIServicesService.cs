using SmartSave.Application.DTOs;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IAIServicesService
    {
        public Task SaveSuggestionAsync(GetSuggestionDto getSuggestionDto, DateTime dateTime, int userId);
        public Task SavePredictionAsync(GetPredictionDto getPredictionDto, DateTime dateTime, int userId);
        public Task<List<GetSuggestionDto>> GetAllSuggestionsAsync(int userId);
        public Task<List<GetPredictionDto>> GetAllPredictionsAsync(int userId);
    }
}
