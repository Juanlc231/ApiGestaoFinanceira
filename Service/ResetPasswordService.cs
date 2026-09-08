using ApiGestaoFinanceira.Connection;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Validate;

namespace ApiGestaoFinanceira.Service
{
    public class ResetPasswordService
    {
        private readonly ConnectionContext _context;
        private readonly ResetPasswordValidate _resetPasswordValidate = new ResetPasswordValidate();

        public ResetPasswordService(ConnectionContext context) => _context = context;

        public async Task<ResetPassword> ResetPasswordInsert(ResetPassword resetPassword)
        {
            _resetPasswordValidate.ValidateResetPassword(resetPassword);

            await _context.AddAsync(resetPassword);
            await _context.SaveChangesAsync();
            return resetPassword;
        }

        public async Task<ResetPassword> BuildResetPassword(int id, string hashedToken) {

            var resetPassword = new ResetPassword
            {
                UserId = id,
                Token = hashedToken,
                Used = false,
                ExpirationDate = DateTime.UtcNow.AddMinutes(10)
            };

            var result = await ResetPasswordInsert(resetPassword);

            return result;
        }
    }
}
