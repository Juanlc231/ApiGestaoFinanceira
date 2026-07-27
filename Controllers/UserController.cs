using ApiGestaoFinanceira.Service;
using ApiGestaoFinanceira.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            var users = _userService.Get();

            return Ok(users);
        }

        [HttpGet("GetUser")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userService.GetById(id);

                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromForm] User user)
        {
            try
            {
                var createdUser = _userService.Insert(user);

                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromForm] User user)
        {
            try
            {
                //var updatedUser = _userService.Update(user, id); adicionar o id como parâmetro se necessário pegando o usuario atual

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
