using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class DebtRepository : IDebtRepository
    {
        private readonly SmartSaveDbContext _context;

        public DebtRepository(SmartSaveDbContext smartSaveDbContext)
            => _context = smartSaveDbContext;

        public async Task AddAsync(Debt debt)
        {
            await _context.Debts.AddAsync(debt);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(Debt debt)
        {
            _context.Debts.Remove(debt);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Debt>> GetAllAsync()
            => await _context.Debts.ToListAsync();

        public async Task<Debt?> GetByIdAsync(int id) 
            => await _context.Debts.FindAsync(id);

        public async Task UpdateAsync(Debt debt)
        {
            _context.Debts.Update(debt);
            await _context.SaveChangesAsync();
        }
    }
}