using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Services;
using System.Security.Claims;

namespace SmartSaveApp.API.Controllers
{
    [Authorize]
    public class GoalController : BaseController
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpPost("create")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateGoalDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                dto.UserId = userId;

                var result = await _goalService.CreateAsync(dto);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Meta creada satisfactoriamente" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetGoalDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var goals = await _goalService.GetAllAsync(userId);
                return Ok(goals);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetGoalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var goal = await _goalService.GetByIdAsync(id);

                if (goal is null)
                    return StatusCode(404, new
                    {
                        status = 404,
                        message = "Meta no encontrada"
                    });

                return Ok(goal);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGoalDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                dto.UserId = userId;

                var result = await _goalService.UpdateAsync(id, dto);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Meta actualizada satisfactoriamente" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var result = await _goalService.DeleteAsync(id, userId);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Meta eliminada satisfactoriamente" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
