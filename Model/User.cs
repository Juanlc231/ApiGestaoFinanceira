using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGestaoFinanceira.Model
{
    [Table("Usuarios")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
