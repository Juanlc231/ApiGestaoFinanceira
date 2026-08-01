using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Dto.Model
{
    public class Expenses
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public EnumExpenseCategories.ExpenseCategories Category { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
