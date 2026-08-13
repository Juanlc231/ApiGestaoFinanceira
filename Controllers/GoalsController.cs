using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiGestaoFinanceira.Service;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.ViewModel;


namespace ApiGestaoFinanceira.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Metas")]
    public class GoalsController : ControllerBase
    {
        private readonly GoalsService _goalsService;
        public GoalsController(GoalsService goalsService) => _goalsService = goalsService;

        [HttpGet("GetAllGoals")]
        public async Task<IActionResult> GetGoals(int page)
        {
            try 
            {
                var idUser = int.Parse(User.FindFirst("Id")!.Value);

                var goals = await _goalsService.GetGoals(idUser, page);

                return Ok(goals);
            } 
            catch(ArgumentException) 
            {
                return BadRequest("Erro ao buscar metas");
            }           
        }

        [HttpGet("GetGoalById")]
        public async Task<IActionResult> GetGoalById(int id)
        {
            try
            {
                var goal = await _goalsService.GetGoalById(id);
                return Ok(goal);
            }
            catch (ArgumentException)
            {
                return BadRequest("Erro ao buscar meta");
            }
        }

        [HttpPost("CreateGoal")]
        public async Task<IActionResult> CreateGoal([FromBody] Goals goal)
        {
            try
            {
                var idUser = int.Parse(User.FindFirst("Id")!.Value);
                var createdGoal = await _goalsService.CreateGoal(goal, idUser);
                return CreatedAtAction(nameof(GetGoalById), new { id = createdGoal.Id }, createdGoal);
            }
            catch (ArgumentException)
            {
                return BadRequest("Erro ao criar meta");
            }
        }

        [HttpPut("UpdateGoal")]
        public async Task<IActionResult> UpdateGoal([FromBody] GoalsViewModel goalViewModel, int id)
        {
            try
            {
                var updatedGoal = await _goalsService.UpdateGoal(id, goalViewModel);
                return Ok(updatedGoal);
            }
            catch (ArgumentException)
            {
                return BadRequest("Erro ao atualizar meta");
            }
        }

        [HttpDelete("DeleteGoal")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            try
            {
                await _goalsService.DeleteGoal(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("Erro ao deletar meta");
            }
        }
    }
}
