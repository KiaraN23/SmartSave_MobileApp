using SmartSave.Core.Entities;

namespace SmartSave.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
