using Microsoft.EntityFrameworkCore;
using SmartSave.Core.Entities;
using SmartSave.Core.Interfaces.Repositories;
using SmartSaveApp.Infrastructure.Data;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartSaveDbContext _context;

        public UserRepository(SmartSaveDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
