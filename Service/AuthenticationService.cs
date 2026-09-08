using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Service
{
    public class AuthenticationService
    {
        private readonly PasswordService _passwordService = new PasswordService();
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly ResetPasswordService _resetPasswordService;
        private readonly HashTokenService _hashTokenService = new HashTokenService();
        private readonly IConfiguration _configuration;

        private static readonly int MaxAttempts = 5;
        private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(15);

        public AuthenticationService(UserService userService, TokenService tokenService, ResetPasswordService resetPasswordService, IConfiguration configuration)
        {
            _userService = userService;
            _tokenService = tokenService;
            _resetPasswordService = resetPasswordService;
            _configuration = configuration;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userService.GetByEmail(email);

            if (user == null || user.Id == 0)
                throw new KeyNotFoundException("Email ou senha inválidos.");

            if (user.Attempts >= MaxAttempts || user.BlockUntil > DateTime.UtcNow)
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

        public async Task<bool> ForgotPassword(string email)
        {
            var hashedToken = "";
            HttpClient httpClient = new HttpClient();

            var user = await _userService.GetByEmail(email);

            if (user.Id != 0)
            {
                var tokenRandom = _tokenService.GenerateTokenResetPassword();
                hashedToken = _hashTokenService.HashToken(tokenRandom);

                await _resetPasswordService.BuildResetPassword(user.Id, hashedToken);
            }

            var token = _tokenService.GenerateTokenEmail(email);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var linkAPIEmail = $"{_configuration["Api-Email-Key:Url"]}/send-email";
            var linkReset = $"{_configuration["FrontEnd:Url"]}/reset-password?token={hashedToken}";

            var item = new
            {
                Title = "Recuperação de Senha",
                To = email,
                Name = "",
                Subject = "Recuperacão de Senha",
                HTMLBody = $"<h1>Olá!</h1><p>Aqui esta o link para redefinir sua senha:{linkReset}</p>"
            };

            var response = await httpClient.PostAsJsonAsync(linkAPIEmail, item);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
