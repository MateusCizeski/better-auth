namespace Domain
{
    public class LoginResultDTO
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
