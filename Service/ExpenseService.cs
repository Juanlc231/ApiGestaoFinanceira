using ApiGestaoFinanceira.Connection;
using Microsoft.EntityFrameworkCore;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Filters;
using ApiGestaoFinanceira.Dto.Utils.Validate;
using ApiGestaoFinanceira.Dto.Utils.Enum;
using ApiGestaoFinanceira.Dto.ViewModel;

namespace ApiGestaoFinanceira.Service
{
    public class ExpenseService
    {
        private readonly ConnectionContext _context;
        private readonly FiltersExpenses _filtersExpenses = new FiltersExpenses();
        private readonly ExpensesValidate _expensesValidate = new ExpensesValidate();

        public ExpenseService(ConnectionContext context) => _context = context;

        public async Task<ExpensesViewModel> GetExpenses(int idUser, int page)
        {
            if (idUser == 0)
                throw new ArgumentException("Id do usuário é necessário");

            if (page < 1)
                throw new ArgumentException("Número da página é necessário");

            var pages = await _context.Expenses.CountAsync(x => x.IdUser == idUser);

            var expenses = await _context.Expenses.Where(e => e.IdUser == idUser)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * 10)
                .Take(10).ToListAsync();

            var viewModel = new ExpensesViewModel
            {
                Expenses = expenses,
                TotalPages = (int)Math.Ceiling(pages / 10.0)
            };

            return viewModel;
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
