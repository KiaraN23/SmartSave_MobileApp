using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Helper;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSaveApp.Infrastructure.Data;

namespace SmartSave.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartSaveDbContext _context;

        public UserRepository(SmartSaveDbContext context) => _context = context;

        public async Task RegisterAsync(User user)
        {
            user.Password = PasswordHasher.Hash(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == email);

            if (user == null) return null;

            if (PasswordHasher.Verify(password, user.Password)) return user;

            return null;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
            => await _context.Users.AnyAsync(e => e.Email == email);
    }
}