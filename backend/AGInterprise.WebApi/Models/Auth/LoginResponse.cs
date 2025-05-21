namespace AGInterprise.WebApi.Models.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}