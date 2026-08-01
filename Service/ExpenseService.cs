using ApiGestaoFinanceira.Connection;
using Microsoft.EntityFrameworkCore;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Filters;
using ApiGestaoFinanceira.Dto.Utils.Validate;
using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Service
{
    public class ExpenseService
    {
        private readonly ConnectionContext _context;
        private readonly FiltersExpenses _filtersExpenses = new FiltersExpenses();
        private readonly ExpensesValidate _expensesValidate = new ExpensesValidate();

        public ExpenseService(ConnectionContext context) => _context = context;

        public async Task<List<Expenses>> GetExpenses(int idUser)
        {
            var expenses = await _context.Expenses.Where(x => x.IdUser == idUser).ToListAsync();
            return expenses;
        }

        public async Task<List<Expenses>> GetFilter(int idUser, DateTime? startDate, DateTime? endDate, EnumExpenseCategories.ExpenseCategories? category)
        {
            if (idUser == 0)
                throw new ArgumentException("Id do usuário é necessário");

            var query = _context.Expenses.AsQueryable();

            if (startDate.HasValue || endDate.HasValue)
                _filtersExpenses.FilterByDate(query, startDate, endDate);

            if (category.HasValue)
                _filtersExpenses.FilterByCategory(query, category);

            return await query.ToListAsync();
        }

        public async Task<Expenses> Insert(Expenses expense)
        {
            _expensesValidate.ValidateExpenses(expense);
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        public void Delete(int id)
        {
            var expense = _context.Expenses.Find(id);

            if (expense == null)
                throw new KeyNotFoundException("Despesa não encontrada");

            _context.Expenses.Remove(expense);
            _context.SaveChangesAsync();
        }
    }
}
