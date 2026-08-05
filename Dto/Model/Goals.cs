using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Dto.Model
{
    public class Goals
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal TargetValue { get; set; }
        public decimal CurrentValue { get; set; }
        public EnumGoalsStatus.GoalStatus Status { get; set; }
        public EnumGoalsCategory.GoalsCategory Category { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Today;
        public DateTime? TargetDate { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
