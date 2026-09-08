using static BCrypt.Net.BCrypt;

namespace ApiGestaoFinanceira.Service
{
    public class HashTokenService
    {
        public string HashToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token é obrigatório.");

            var tokenHash = HashPassword(token);

            return tokenHash;
        }
    }
}
