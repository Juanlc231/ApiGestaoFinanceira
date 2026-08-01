using ApiGestaoFinanceira.Connection;
using Microsoft.EntityFrameworkCore;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Validate;

namespace ApiGestaoFinanceira.Service
{
    public class UserService
    {
        private readonly ConnectionContext _context;
        private readonly UserValidate _userValidate = new UserValidate();
        private readonly PasswordValidate _passwordValidate = new PasswordValidate();
        private readonly PasswordService _passwordService = new PasswordService();

        public UserService(ConnectionContext context) => _context = context;

        public async Task<List<User>> Get() 
        { 
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetById(int id) {

            if (id <= 0)
                throw new ArgumentException("Id de usuário inválido.");

            User user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException("Usuário não encontrado.");

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email é obrigatório.");

            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException("Usuário não encontrado.");

            return user;
        }

        public async Task<User> Insert(User user)
        {
            _userValidate.Validate(user);
            user.Password = _passwordService.PasswordHash(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<int> Update(User updatedUser, int id) 
        {
            _userValidate.Validate(updatedUser);

            var user =  await GetById(id);

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task ResetPassword(string email, string newPassword, string confirmNewPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _passwordValidate.Validate(newPassword, confirmNewPassword);

            user.Password = newPassword;
            await _context.SaveChangesAsync();
        }
    }
}