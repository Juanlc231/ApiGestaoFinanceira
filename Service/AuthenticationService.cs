using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Service
{
    public class AuthenticationService
    {
        private readonly PasswordService _passwordService = new PasswordService();
        private readonly UserService _userService;

        public AuthenticationService(UserService userService) => _userService = userService;

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userService.GetByEmail(email);

            if (!_passwordService.VerifyPassword(password, user.Password))
                throw new ArgumentException("Email ou senha inválidos.");

            return user;
        }
    }
}
