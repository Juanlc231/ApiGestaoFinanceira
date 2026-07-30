using ApiGestaoFinanceira.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ApiGestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class LoginController: ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AuthenticationService _authenticationService;

        public LoginController(TokenService tokenService, AuthenticationService authenticationService)
        {
            _tokenService = tokenService;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string email, string password)
        {
            try {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return BadRequest("Credenciais inválidas");

                var user = await _authenticationService.Authenticate(email, password);

                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            } 
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            } 
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        //add metodo de reset senha
        //add metodo de logout
    }
}
