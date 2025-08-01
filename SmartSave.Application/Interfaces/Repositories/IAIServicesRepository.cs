using SmartSave.Core.Entities;

namespace SmartSave.Application.Interfaces.Repositories
{
    public interface IAIServicesRepository
    {
        public Task SaveSuggestionAsync(Suggestion suggestion);
        public Task SavePredictionAsync(Prediction prediction);
    }
}
