using SmartSave.Application.DTOs;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IDebtService
    {
        public Task<BasicResponse> AddAsync(CreateDebtDto dto);
        public Task<BasicResponse> UpdateAsync(int id, CreateDebtDto dto);
        public Task<BasicResponse> DeleteAsync(int id, int userId);
        public Task<IEnumerable<GetDebtDto>> GetAllAsync(int userId);
        public Task<GetDebtDto?> GetByIdAsync(int id);
    }
}
