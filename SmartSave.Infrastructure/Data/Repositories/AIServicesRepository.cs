using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class AIServicesRepository : IAIServicesRepository
    {
        private readonly SmartSaveDbContext _context;

        public AIServicesRepository(SmartSaveDbContext smartSaveDbContext)
        {
            _context = smartSaveDbContext;
        }

        public async Task SaveSuggestionAsync(Suggestion suggestion)
        {
            await _context.Suggestions.AddAsync(suggestion);
            await _context.SaveChangesAsync();
        }

        public async Task SavePredictionAsync(Prediction prediction)
        {
            await _context.Predictions.AddAsync(prediction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Prediction>> GetAllPredictionsAsync(int userId)
        {
            return await _context.Predictions
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Suggestion>> GetAllSuggestionsAsync(int userId)
        {
            return await _context.Suggestions
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }
    }
}
