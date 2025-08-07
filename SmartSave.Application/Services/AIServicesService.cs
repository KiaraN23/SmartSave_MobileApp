using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Services
{
    public class AIServicesService : IAIServicesService
    {
        private readonly IAIServicesRepository _repository;

        public AIServicesService(IAIServicesRepository repository) => _repository = repository;

        public async Task SavePredictionAsync(GetPredictionDto getPredictionDto, DateTime dateTime, int userId)
        {
            var prediction = new Prediction
            {
                UserId = userId,
                PredictionMessage = getPredictionDto.PredictionMessage,
                CreatedAt = dateTime
            };

            await _repository.SavePredictionAsync(prediction);
        }

        public async Task SaveSuggestionAsync(GetSuggestionDto getSuggestionDto, DateTime dateTime, int userId)
        {
            var suggestion = new Suggestion
            {
                UserId = userId,
                SuggestionMessage = getSuggestionDto.SuggestionMessage,
                CreatedAt = dateTime
            };

            await _repository.SaveSuggestionAsync(suggestion);
        }

        public async Task<List<GetSuggestionDto>> GetAllSuggestionsAsync(int userId)
        {
            var suggestions = await _repository.GetAllSuggestionsAsync(userId);

            return suggestions.Select(s => new GetSuggestionDto
            {
                SuggestionMessage = s.SuggestionMessage
            }).ToList();
        }

        public async Task<List<GetPredictionDto>> GetAllPredictionsAsync(int userId)
        {
            var predictions = await _repository.GetAllPredictionsAsync(userId);

            return predictions.Select(p => new GetPredictionDto
            {
                PredictionMessage = p.PredictionMessage
            }).ToList();
        }
    }
}
