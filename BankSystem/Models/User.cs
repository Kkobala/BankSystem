using BankSystem.Db.Entities;

namespace BankSystem.Models
{
	public class User
	{
		public string? Name { get; set; }
		public string? LastName { get; set; }
		public string PersonalNumber { get; set; }
		public string? Email { get; set; }
		public string BirthDate { get; set; }
		public List<Account> Accounts { get; set; }

        public User()
        {
            Accounts = new List<Account>();
        }
    }
}
