using ApiGestaoFinanceira.Dto.Utils.Enum;

namespace ApiGestaoFinanceira.Dto.ViewModel
{
    public class GoalsViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal TargetValue { get; set; }
        public decimal CurrentValue { get; set; }
        public EnumGoalsStatus.GoalStatus Status { get; set; }
        public EnumGoalsCategory.GoalsCategory Category { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
