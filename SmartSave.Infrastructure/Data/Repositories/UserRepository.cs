using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSaveApp.Infrastructure.Data;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartSaveDbContext _context;

        public UserRepository(SmartSaveDbContext context) => _context = context;

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
            => await _context.Users.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
    }
}