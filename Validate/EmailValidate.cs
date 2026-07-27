namespace ApiGestaoFinanceira.Validate
{
    public class EmailValidate
    {
        public void Validate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email é obrigatório.");

            if (!email.Contains("@"))
                throw new Exception("Este email é inválido.");
        }
    }
}
