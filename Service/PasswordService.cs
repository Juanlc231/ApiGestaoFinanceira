using static BCrypt.Net.BCrypt;

namespace ApiGestaoFinanceira.Service
{
    public class PasswordService
    {

        public string HashPassword(string password){

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha é obrigatória.");

            string passwordHash = HashPassword(password);

            return passwordHash;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Senha ou hash de senha inválidos.");

            return Verify(password, passwordHash);
        }
    }
}
