using SmartSave.Core.Entities;

namespace SmartSave.Application.Interfaces.Repositories
{
    public interface IAISuggestionRepository
    {
        public Task SaveSuggestionAsync(Suggestion suggestion);
    }
}
