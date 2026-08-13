using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Dto.ViewModel
{
    public class ExpensesViewModel
    {
        public List<Expenses> Expenses { get; set; } = new();
        public int TotalPages { get; set; }
    }
}
