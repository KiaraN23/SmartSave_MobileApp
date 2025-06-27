using Microsoft.EntityFrameworkCore;
using SmartSave.Application.Helper;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Core.Entities;
using SmartSave.Infrastructure.Data.Contexts;

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

        public async Task<bool> ResetPassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (!PasswordHasher.Verify(currentPassword, user.Password)) return false;

            user.Password = PasswordHasher.Hash(newPassword);
            _context.Entry(user).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}