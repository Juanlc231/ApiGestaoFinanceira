using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Dto.Utils.Validate
{
    public class UserValidate
    {
        private readonly EmailValidate _emailValidate = new EmailValidate();
        private readonly PasswordValidate _passwordValidate = new PasswordValidate();

        public void Validate(User user)
        {
            if (user == null)
                throw new Exception("Usuário não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(user.Name))
                throw new Exception("Nome do usuário é obrigatório.");

            _emailValidate.Validate(user.Email);

            _passwordValidate.Validate(user.Password, user.ConfirmPassword);
        }
    }
}
