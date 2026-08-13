using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Dto.ViewModel
{
    public class UserViewModel
    {
        public List<User> Users { get; set; } = new();
        public int Page { get; set; }
    }
}
