namespace BankSystem.Models.Requests
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PersonalNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
}
