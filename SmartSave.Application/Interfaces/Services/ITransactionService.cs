using SmartSave.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSave.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<BasicResponse> CreateAsync(CreateTransactionDto dto);
        Task<GetTransactionDto?> GetByIdAsync(int id);
        Task<IEnumerable<GetTransactionDto>> GetAllAsync(int userId);
        Task<BasicResponse> DeleteAsync(int id, int userId);
        Task<BasicResponse> UpdateAsync(int id, CreateTransactionDto dto);

    }
}
