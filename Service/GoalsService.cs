using ApiGestaoFinanceira.Connection;
using ApiGestaoFinanceira.Dto.Model;
using ApiGestaoFinanceira.Dto.Utils.Enum;
using ApiGestaoFinanceira.Dto.Utils.Validate;
using ApiGestaoFinanceira.Dto.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoFinanceira.Service
{
    public class GoalsService
    {
        private readonly ConnectionContext _context;
        private readonly GoalsValidate _goalsValidate = new GoalsValidate();

        public GoalsService(ConnectionContext context) => _context = context;

        public async Task<ResultGoals> GetGoals(int idUser, int page)
        {
            if (idUser == 0)
                throw new ArgumentNullException("id do usuario nao informado");

            if (page == 0) 
                throw new ArgumentNullException("pagina nao informada");

            var itens = await _context.Goals.CountAsync(x => x.IdUser == idUser);

            var goals = await _context.Goals.Where(x => x.IdUser == idUser).
                OrderBy(x => x.Id).
                Skip((page - 1) * 10).
                Take(10).ToListAsync();

            return new ResultGoals 
            { 
                Goals = goals, 
                Pages = (int)Math.Ceiling((double)itens / 10)
            };
        }

        public async Task<Goals> GetGoalById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id da meta nao informado");

            var goal = await _context.Goals.Where(x => x.Id == id).FirstAsync();

            if (goal == null)
                throw new ArgumentNullException("Meta não encontrada");

            return goal;
        }

        public async Task<Goals> CreateGoal(Goals goal, int idUser)
        {
            _goalsValidate.ValidateGoal(goal, idUser);

            goal.IdUser = idUser;

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<Goals> UpdateGoal(int id, GoalsViewModel goalViewModel)
        {
            if (id == 0)
                throw new ArgumentNullException("id da meta nao informado");

            var goal = await GetGoalById(id);

            _goalsValidate.ValidateGoalUpdate(goalViewModel, goal);

            goal.Title = goalViewModel.Title;
            goal.Description = goalViewModel.Description;
            goal.TargetValue = goalViewModel.TargetValue;
            goal.CurrentValue = goalViewModel.CurrentValue;
            goal.Status = goalViewModel.Status;
            goal.Category = goalViewModel.Category;
            goal.TargetDate = goalViewModel.TargetDate;
            goal.EndTime = goalViewModel.EndTime;
            goal.IsCompleted = goalViewModel.IsCompleted;

            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task DeleteGoal(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id da meta nao informado");

            var goal = await GetGoalById(id);

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }
    }
}
