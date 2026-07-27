namespace ApiGestaoFinanceira.Validate
{
    public class PasswordValidate
    {
        public void Validate(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Senha é obrigatória.");

            if (password.Length < 8)
                throw new Exception("Senha deve ter no mínimo 8 caracteres.");

            if (!password.Any(char.IsUpper))
                throw new Exception("Senha deve conter pelo menos uma letra maiúscula.");

            if (!password.Any(char.IsLower))
                throw new Exception("Senha deve conter pelo menos uma letra minúscula.");

            if (!password.Any(char.IsDigit))
                throw new Exception("Senha deve conter pelo menos um número.");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new Exception("Senha deve conter pelo menos um caractere especial.");

            if (password != confirmPassword)
                throw new Exception("As senhas não coincidem.");
        }
    }
}
