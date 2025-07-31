using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Services
{
    public class AISuggestionService : IAISuggestionService
    {
        private readonly IAISuggestionRepository _aISuggestionRepository;

        public AISuggestionService(IAISuggestionRepository suggestionRepository)
        {
            _aISuggestionRepository = suggestionRepository;
        }

        public async Task SaveSuggestionAsync(GetSuggestionDto getSuggestionDto, DateTime dateTime, int userId)
        {
            var suggestion = new Suggestion
            {
                UserId = userId,
                SuggestionMessage = getSuggestionDto.SuggestionMessage,
                CreatedAt = dateTime
            };

            await _aISuggestionRepository.SaveSuggestionAsync(suggestion);
        }
    }
}
