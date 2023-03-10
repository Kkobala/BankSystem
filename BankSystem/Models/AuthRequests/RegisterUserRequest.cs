namespace BankSystem.Models.AuthRequests
{
	public class RegisterUserRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
