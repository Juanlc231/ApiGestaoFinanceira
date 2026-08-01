using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Service;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            try {
                if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
                    return BadRequest("Credenciais inválidas");

                var user = await _authenticationService.Authenticate(loginModel.Email, loginModel.Password);

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
