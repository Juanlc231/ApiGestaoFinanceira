using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Dto.Utils.Filters
{
    public class FiltersExpenses
    {
        public IQueryable<Expenses> FilterByDate(IQueryable<Expenses> expenses, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
                expenses = expenses.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue)
                expenses = expenses.Where(e => e.Date <= endDate.Value);
            
            return expenses;
        }

        public IQueryable<Expenses> FilterByCategory(IQueryable<Expenses> expenses, EnumExpenseCategories.ExpenseCategories? category)
        {
            if (category.HasValue)
                expenses = expenses.Where(e => e.Category == category.Value);

            return expenses;
        }
    }
}
