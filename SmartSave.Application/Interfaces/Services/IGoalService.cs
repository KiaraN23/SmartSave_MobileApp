using Microsoft.Extensions.Options;
using SmartSave.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IGoalService
    {
        Task<BasicResponse> CreateAsync(CreateGoalDto dto); 
        Task<GetGoalDto?> GetByIdAsync(int id);
        Task<IEnumerable<GetGoalDto>> GetAllAsync(int userId);
        Task<BasicResponse> UpdateAsync(int id, UpdateGoalDto dto);
        Task<BasicResponse> DeleteAsync(int id, int userId);
    }
}
