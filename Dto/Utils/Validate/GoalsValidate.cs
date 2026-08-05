using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.ViewModel;

namespace ApiGestaoFinanceira.Dto.Utils.Validate
{
    public class GoalsValidate
    {
        public void ValidateGoal(Goals goal, int idUser)
        {
            if (goal == null)
                throw new ArgumentNullException("meta nao informada");

            if (idUser == 0)
                throw new ArgumentNullException("id do usuario nao informado");

            if (string.IsNullOrWhiteSpace(goal.Title))
                throw new ArgumentException("titulo da meta nao informado");

            if (goal.TargetValue <= 0)
                throw new ArgumentException("valor alvo da meta deve ser maior que zero");

            if (goal.CurrentValue < 0)
                throw new ArgumentException("valor atual da meta nao pode ser negativo");

            if (goal.StartTime < DateTime.MinValue)
                throw new ArgumentException("data de inicio da meta nao pode ser invalida");

            if (goal.TargetDate < goal.StartTime)
                throw new ArgumentException("data alvo da meta nao pode ser anterior à data de inicio");

            if (goal.EndTime < goal.StartTime)
                throw new ArgumentException("data de termino da meta nao pode ser anterior ou igual à data de inicio");
        }

        public void ValidateGoalUpdate(GoalsViewModel goalViewModel, Goals goal)
        {
            if (goalViewModel == null)
                throw new ArgumentNullException("dados da meta nao informados");

            if (goal == null)
                throw new ArgumentNullException("meta nao encontrada");

            if (string.IsNullOrWhiteSpace(goalViewModel.Title))
                throw new ArgumentException("titulo da meta nao informado");

            if (goalViewModel.TargetValue <= 0)
                throw new ArgumentException("valor alvo da meta deve ser maior que zero");

            if (goalViewModel.CurrentValue < 0)
                throw new ArgumentException("valor atual da meta nao pode ser negativo");

            if (goalViewModel.TargetDate < goal.StartTime)
                throw new ArgumentException("data alvo da meta nao pode ser anterior à data de inicio");

            if (goalViewModel.EndTime < goal.StartTime)
                throw new ArgumentException("data de termino da meta nao pode ser anterior ou igual à data de inicio");
        }
    }
}
