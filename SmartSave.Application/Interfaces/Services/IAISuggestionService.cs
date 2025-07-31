using SmartSave.Application.DTOs;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IAISuggestionService
    {
        public Task SaveSuggestionAsync(GetSuggestionDto getSuggestionDto, DateTime dateTime, int userId); 
    }
}
