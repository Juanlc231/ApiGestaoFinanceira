using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Service
{
    public class AuthenticationService
    {
        private static readonly int MaxAttempts = 5;
        private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(15);

        private readonly PasswordService _passwordService = new PasswordService();
        private readonly UserService _userService;

        public AuthenticationService(UserService userService) => _userService = userService;

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userService.GetByEmail(email);

            if (user == null)
                throw new KeyNotFoundException("Email ou senha inválidos.");

            if(user.Attempts >= MaxAttempts || user.BlockUntil > DateTime.UtcNow)
                throw new InvalidOperationException("Conta bloqueada por tentativas inválidas, tente novamente mais tarde.");

            if (!_passwordService.VerifyPassword(password, user.Password))
            {
                user.Attempts += 1;

                if (user.Attempts >= MaxAttempts)
                    user.BlockUntil = DateTime.UtcNow.Add(BlockDuration);

                await _userService.Update(user, user.Id);

                throw new ArgumentException("Email ou senha inválidos.");
            }

            user.Attempts = 0;
            user.BlockUntil = null;
            await _userService.Update(user, user.Id);

            return user;
        }
    }
}
