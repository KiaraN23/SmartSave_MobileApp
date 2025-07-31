using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class AISuggestionRepository : IAISuggestionRepository
    {
        private readonly SmartSaveDbContext _context;

        public AISuggestionRepository(SmartSaveDbContext smartSaveDbContext)
        {
            _context = smartSaveDbContext;
        }

        public async Task SaveSuggestionAsync(Suggestion suggestion)
        {
            await _context.Suggestions.AddAsync(suggestion);
            await _context.SaveChangesAsync();
        }
    }
}
