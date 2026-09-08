namespace ApiGestaoFinanceira.Dto.Model
{
    public class ResetPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool Used { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
