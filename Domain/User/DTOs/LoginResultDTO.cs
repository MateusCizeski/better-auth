namespace Domain.User.DTOs
{
    public class LoginResultDTO
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
