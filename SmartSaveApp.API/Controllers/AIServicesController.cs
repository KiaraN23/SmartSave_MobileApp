using Microsoft.AspNetCore.Mvc;
using SmartSave.Application.DTOs;
using SmartSave.Application.Helper;
using SmartSave.Application.Interfaces.Services;
using System.Security.Claims;

namespace SmartSaveApp.API.Controllers
{
    public class AIServicesController : BaseController
    {
        private readonly IGoalService _goalService;
        private readonly ITransactionService _transactionService;
        private readonly IOpenRouterApiService _openRouterApiService;
        private readonly IAIServicesService _suggestionService;

        public AIServicesController(IGoalService goalService,
                        ITransactionService transactionService, 
                        IOpenRouterApiService openRouterApiService,
                        IAIServicesService suggestionService)
        {
            _goalService = goalService;
            _transactionService = transactionService;
            _openRouterApiService = openRouterApiService;
            _suggestionService = suggestionService;
        }

        [HttpGet("suggestion")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSuggestionDto))]
        public async Task<IActionResult> GetSuggestion()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var goals = await _goalService.GetAllAsync(userId);
                var transactions = await _transactionService.GetAllAsync(userId);

                var prompt = PromptBuilder.BuildSuggestionPrompt(goals, transactions);

                var suggestionMessage = await _openRouterApiService.SendMessageAsync(prompt);

                var response = new GetSuggestionDto { SuggestionMessage = suggestionMessage };

                await _suggestionService.SaveSuggestionAsync(response, DateTime.Now, userId);

                return Ok(response);
            } catch (Exception e)
            {
                return InternalServerError(e.Message);
            }
        }

        [HttpGet("prediction")]
        public async Task<IActionResult> GetPrediction()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var goals = await _goalService.GetAllAsync(userId);
                var transactions = await _transactionService.GetAllAsync(userId);

                var prompt = PromptBuilder.BuildPredictionPrompt(goals, transactions);

                var predictionMessage = await _openRouterApiService.SendMessageAsync(prompt);

                var response = new GetPredictionDto { PredictionMessage = predictionMessage };

                await _suggestionService.SavePredictionAsync(response, DateTime.Now, userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalServerError(e.Message);
            }
        }
    }
}
