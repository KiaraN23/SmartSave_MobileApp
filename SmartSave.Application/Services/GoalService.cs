using Microsoft.AspNetCore.Http;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Services
{
    public class GoalService : IGoalService
    {
        public IGoalRepository _goalRepository;
        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<BasicResponse> CreateAsync(CreateGoalDto dto)
        {
            if (dto.ObjectiveAmount <= 0)
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "El monto objetivo debe ser mayor que cero."
                };

            if (dto.CurrentAmount < 0)
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "El monto actual no puede ser negativo."
                };

            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Deadline.ToString()))
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "El nombre o la fecha son requeridos."
                };

            var goal = new Goal
            {
                UserId = dto.UserId,
                Name = dto.Name,
                ObjectiveAmount = dto.ObjectiveAmount,
                CurrentAmount = dto.CurrentAmount,
                Deadline = dto.Deadline
            };

            await _goalRepository.AddAsync(goal);

            return new BasicResponse();
        }

        public async Task<IEnumerable<GetGoalDto>> GetAllAsync(int userId)
        {
            var goals = await _goalRepository.GetAllAsync();
            return goals
                .Where(g => g.UserId == userId)
                .Select(g => new GetGoalDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    ObjectiveAmount = g.ObjectiveAmount,
                    CurrentAmount = g.CurrentAmount,
                    Deadline = g.Deadline
                });
        }

        public async Task<GetGoalDto?> GetByIdAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            if (goal is null) return null;

            return new GetGoalDto
            {
                Id = goal.Id,
                Name = goal.Name,
                ObjectiveAmount = goal.ObjectiveAmount,
                CurrentAmount = goal.CurrentAmount,
                Deadline = goal.Deadline
            };
        }

        public async Task<BasicResponse> UpdateAsync(int id, UpdateGoalDto dto)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            if (goal == null || goal.UserId != dto.UserId)
            {
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Meta no encontrada o no pertenece al usuario."
                };
            }

            goal.UserId = dto.UserId;
            goal.Name = dto.Name;
            goal.ObjectiveAmount = dto.ObjectiveAmount;
            goal.CurrentAmount = dto.CurrentAmount;
            goal.Deadline = dto.Deadline;

            await _goalRepository.UpdateAsync(goal);
            return new BasicResponse();
        }

        public async Task<BasicResponse> DeleteAsync(int id, int userId)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            if (goal == null || goal.UserId != userId)
            {
                return new BasicResponse
                {
                    HasError = true,
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Meta no encontrada o no pertenece al usuario."
                };
            }

            await _goalRepository.DeleteAsync(id);
            return new BasicResponse();
        }
    }                       
}
