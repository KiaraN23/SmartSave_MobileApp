using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Services;
using System.Security.Claims;

namespace SmartSaveApp.API.Controllers
{
    [Authorize]
    public class DebtController : BaseController
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService) => _debtService = debtService;

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDebtDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                dto.UserId = userId;

                var result = await _debtService.AddAsync(dto);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Debt created successfully" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetDebtDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var debts = await _debtService.GetAllAsync(userId);
                return Ok(debts);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDebtDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var debt = await _debtService.GetByIdAsync(id);

                if (debt is null)
                    return StatusCode(404, new
                    {
                        status = 404,
                        message = "Transaction not found"
                    });

                return Ok(debt);
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
                var result = await _debtService.DeleteAsync(id, userId);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Debt deleted successfully" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDebtDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                dto.UserId = userId;

                var result = await _debtService.UpdateAsync(id, dto);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Debt updated successfully" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}