using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SmartSaveDbContext _context;

        public TransactionRepository(SmartSaveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Transaction updatedTransaction)
        {
            var transactionToUpdate = await _context.Transactions.FindAsync(id);
            
            if (transactionToUpdate is not null)
            {
                transactionToUpdate.Amount = updatedTransaction.Amount;
                transactionToUpdate.Description = updatedTransaction.Description;
                transactionToUpdate.Date = updatedTransaction.Date;
                transactionToUpdate.Type = updatedTransaction.Type;
                await _context.SaveChangesAsync();
            }
        }
    }
}
