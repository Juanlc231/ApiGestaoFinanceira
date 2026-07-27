using ApiGestaoFinanceira.Connection;
using ApiGestaoFinanceira.Validate;
using ApiGestaoFinanceira.Model;

namespace ApiGestaoFinanceira.Service
{
    public class UserService
    {
        private readonly ConnectionContext _context;
        private readonly UserValidate _userValidate = new UserValidate();
        private readonly PasswordValidate _passwordValidate = new PasswordValidate();

        public UserService(ConnectionContext context) => _context = context;

        public List<User> Get() 
        { 
            var users = _context.Users.ToList();

            return users;
        }

        public User GetById(int id) {

            if (id <= 0)
                throw new ArgumentException("Id de usuário inválido.");

            User user = _context.Users.Find(id) ?? throw new KeyNotFoundException("Usuário não encontrado.");

            return user;
        }

        public User Insert(User user)
        {
            _userValidate.Validate(user);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public int Update(User updatedUser, int id) 
        {
            _userValidate.Validate(updatedUser);

            var user = GetById(id);

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            _context.SaveChanges();

            return user.Id;
        }

        public void ResetPassword(int id, string newPassword, string confirmNewPassword)
        {
            var user = GetById(id);
            _passwordValidate.Validate(newPassword, confirmNewPassword);

            user.Password = newPassword;

            _context.SaveChanges();
        }
    }
}
