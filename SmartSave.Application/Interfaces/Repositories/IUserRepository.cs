using SmartSave.Core.Entities;

namespace SmartSave.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndPasswordAsync(string email, string password);
    }
}
