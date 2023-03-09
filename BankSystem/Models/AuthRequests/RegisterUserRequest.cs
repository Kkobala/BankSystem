namespace BankSystem.Models.AuthRequests
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
