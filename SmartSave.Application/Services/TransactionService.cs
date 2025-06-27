using Microsoft.AspNetCore.Http;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;
using SmartSave.Core.Enums;

namespace SmartSave.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<BasicResponse> CreateAsync(CreateTransactionDto dto)
        {
            if (dto.Amount <= 0)
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Amount must be greater than 0."
                };

            if (string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.Date.ToString()))
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Description or date are required."
                };

            if(!Enum.IsDefined(typeof(TransactionType), dto.Type))
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid transaction type. Must be either 'Income (0)' or 'Expense (1)'."
                };

            var transaction = new Transaction
            {
                UserId = dto.UserId,
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date,
                Type = dto.Type
            };

            await _transactionRepository.AddAsync(transaction);

            return new BasicResponse();
        }

        public async Task<IEnumerable<GetTransactionDto>> GetAllAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions
                .Where(t => t.UserId == userId)
                .Select(t => new GetTransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    Type = t.Type
                });
        }

        public async Task<GetTransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction is null) return null;

            return new GetTransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                Type = transaction.Type
            };
        }

        public async Task<BasicResponse> DeleteAsync(int id, int userId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null || transaction.UserId != userId)
            {
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Transaction not found or does not belong to the user."
                };
            }

            await _transactionRepository.DeleteAsync(id);
            return new BasicResponse();
        }
    }
}
