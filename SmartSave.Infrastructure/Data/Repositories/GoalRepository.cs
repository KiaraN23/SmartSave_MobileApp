using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        public SmartSaveDbContext _context;
        public GoalRepository(SmartSaveDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Goal>> GetAllAsync()
        {
            return await _context.Goals.ToListAsync();
        }

        public async Task<Goal?> GetByIdAsync(int id)
        {
            return await _context.Goals.FindAsync(id);
        }

        public async Task AddAsync(Goal goal)
        {
            await _context.Goals.AddAsync(goal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Goal goal)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync();
        }


        public async Task<Goal?> DeleteAsync(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
                await _context.SaveChangesAsync();
                return goal;
            }
            return null;
        }
    }
}
