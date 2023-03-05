namespace BankSystem.Models.AuthRequests
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}
