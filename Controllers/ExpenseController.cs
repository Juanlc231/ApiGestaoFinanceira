using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Service;
using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Despesas")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService) => _expenseService = expenseService;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExpenses(int page)
        {
            var idUser = int.Parse(User.FindFirst("Id")!.Value);

            var expenses = await _expenseService.GetExpenses(idUser, page);
            return Ok(expenses);
        }

        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter(DateTime? startDate, DateTime? endDate, EnumExpenseCategories.ExpenseCategories? category)
        {
            try
            {
                var idUser = int.Parse(User.FindFirst("Id")!.Value);

                if(idUser == 0)
                    return BadRequest("ID do usuário não encontrado");

                var expenses = await _expenseService.GetFilter(idUser, startDate, endDate, category);
                return Ok(expenses);
            }
            catch (KeyNotFoundException) 
            { 
                return BadRequest("Erro ao filtrar despesas"); 
            }
        }

        [HttpPost("CreateExpense")]
        public async Task<IActionResult> CreateExpense([FromForm] Expenses expense)
        {
            try 
            {
                expense.IdUser = int.Parse(User.FindFirst("Id")!.Value);

                var item = await _expenseService.Insert(expense);

                return CreatedAtAction(nameof(GetAllExpenses), new { id = expense.Id }, expense);
            }
            catch (ArgumentException e)
            {   
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteExpense/{id}")]
        public IActionResult DeleteExpense(int id)
        {
            try 
            {
                _expenseService.Delete(id);
                return NoContent();
            } 
            catch (KeyNotFoundException e)
            { 
                return NotFound(e.Message);
            }
        }
    }
}
