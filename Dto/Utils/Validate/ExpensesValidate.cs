using ApiGestaoFinanceira.Dto.Model;

namespace ApiGestaoFinanceira.Dto.Utils.Validate
{
    public class ExpensesValidate
    {
        public void ValidateExpenses(Expenses expenses) 
        {
            if(expenses == null)
                throw new ArgumentNullException("Despesa é necessária");

            if(expenses.IdUser == 0)
                throw new ArgumentException("Id do usuário é necessário");

            if (expenses.Value < 0)
                throw new ArgumentException("Valor da despesa é necessário");

            if (expenses.Date == DateTime.MinValue)
                throw new ArgumentException("Data da despesa é necessária");
        }
    }
}
