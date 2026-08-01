using System.ComponentModel.DataAnnotations.Schema;
using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Dto.Model
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;
        public EnumUser.RoleUser Role { get; set; } = EnumUser.RoleUser.User;
    }
}
