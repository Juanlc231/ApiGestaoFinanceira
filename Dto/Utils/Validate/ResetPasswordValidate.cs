using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Dto.Utils.Validate
{
    public class ResetPasswordValidate
    {
        public void ValidateResetPassword(ResetPassword resetPassword) {

            if (resetPassword.UserId <= 0)
                throw new ArgumentException("Id de usuário inválido.");

            if (string.IsNullOrEmpty(resetPassword.Token))
                throw new ArgumentException("Token é obrigatório.");
        }
    }
}
