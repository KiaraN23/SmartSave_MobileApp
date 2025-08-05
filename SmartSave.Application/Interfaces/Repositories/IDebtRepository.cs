using SmartSave.Core.Entities;

namespace SmartSave.Application.Interfaces.Repositories
{
    public interface IDebtRepository
    {
        public Task<IEnumerable<Debt>> GetAllAsync();
        public Task<Debt?> GetByIdAsync(int id); 
        public Task AddAsync(Debt debt);
        public Task UpdateAsync(Debt debt);
        public Task DeleteAsync(Debt debt);
    }
}
