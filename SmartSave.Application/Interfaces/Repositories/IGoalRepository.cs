using SmartSave.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSave.Application.Interfaces.Repositories
{
    public interface IGoalRepository
    {
        Task<IEnumerable<Goal>> GetAllAsync();
        Task<Goal?> GetByIdAsync(int id);
        Task AddAsync(Goal goal);
        Task UpdateAsync(Goal goal);
        Task<Goal?> DeleteAsync(int id);
    }
}
