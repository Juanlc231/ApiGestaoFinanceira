using ApiGestaoFinanceira.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService = new UserService();

        [HttpGet("Usuarios")]
        public IActionResult Get()
        {

            return Ok("");
        }
    }
}
