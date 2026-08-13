namespace ApiGestaoFinanceira.Dto.Model
{
    public class ResultGoals
    {
        public List<Goals> Goals { get; set; } = new();
        public int Pages { get; set; }
    }
}
