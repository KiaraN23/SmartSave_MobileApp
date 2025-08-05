using Microsoft.AspNetCore.Http;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Services
{
    public class DebtService : IDebtService
    {
        private readonly IDebtRepository _repository;

        public DebtService(IDebtRepository repository) => _repository = repository;

        public async Task<BasicResponse> AddAsync(CreateDebtDto dto)
        {
            if (dto.TotalAmount <= 0 || dto.AmountPaid < 0 || dto.AmountPaid > dto.TotalAmount)
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Total amount must be greater than 0, and amount paid must be 0 or greater and smaller or equal than total amount."
                };

            if (string.IsNullOrWhiteSpace(dto.Creditor) || string.IsNullOrWhiteSpace(dto.Deadline.ToString()))
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Creditor and deadline are required."
                };

            var debt = new Debt
            {
                UserId = dto.UserId,
                Creditor = dto.Creditor,
                TotalAmount = dto.TotalAmount,
                AmountPaid = dto.AmountPaid,
                Description = dto.Description,
                Deadline = dto.Deadline,
                RemainingAmount = dto.TotalAmount - dto.AmountPaid,
            };

            await _repository.AddAsync(debt);

            return new BasicResponse();
        }

        public async Task<BasicResponse> DeleteAsync(int id, int userId)
        {
            var debt = await _repository.GetByIdAsync(id);
            if (debt == null || debt.UserId != userId)
            {
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Debt not found or does not belong to the user."
                };
            }

            await _repository.DeleteAsync(debt);
            return new BasicResponse();
        }

        public async Task<IEnumerable<GetDebtDto>> GetAllAsync(int userId)
        {
            var debts = await _repository.GetAllAsync();

            return debts
                .Where(d => d.UserId == userId)
                .Select(d => new GetDebtDto
                {
                    Id = d.Id,
                    Creditor = d.Creditor,
                    TotalAmount = d.TotalAmount,
                    AmountPaid = d.AmountPaid,
                    RemainingAmount = d.RemainingAmount,
                    Description = d.Description,
                    Deadline = d.Deadline
                });
        }

        public async Task<GetDebtDto?> GetByIdAsync(int id)
        {
            var debt = await _repository.GetByIdAsync(id);

            if (debt is not null)
            {
                return new GetDebtDto
                {
                    Id = id,
                    Creditor = debt.Creditor,
                    TotalAmount = debt.TotalAmount,
                    AmountPaid = debt.AmountPaid,
                    RemainingAmount = debt.RemainingAmount,
                    Description = debt.Description,
                    Deadline = debt.Deadline
                };
            }

            return null;
        }

        public async Task<BasicResponse> UpdateAsync(int id, CreateDebtDto dto)
        {
            var existingDebt = await _repository.GetByIdAsync(id);

            if (existingDebt == null || existingDebt.UserId != dto.UserId)
            {
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Debt not found or does not belong to the user."
                };
            }

            if (dto.TotalAmount <= 0 || dto.AmountPaid < 0)
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Total amount must be greater than 0 and amount paid must be 0 or greater."
                };

            if (string.IsNullOrWhiteSpace(dto.Creditor) || string.IsNullOrWhiteSpace(dto.Deadline.ToString()))
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Creditor and deadline are required."
                };

            existingDebt.Id = id;
            existingDebt.Creditor = dto.Creditor;
            existingDebt.TotalAmount = dto.TotalAmount;
            existingDebt.AmountPaid = dto.AmountPaid;
            existingDebt.RemainingAmount = dto.TotalAmount - dto.AmountPaid;
            existingDebt.Description = dto.Description;
            existingDebt.Deadline = dto.Deadline;

            await _repository.UpdateAsync(existingDebt);

            return new BasicResponse();
        }
    }
}
