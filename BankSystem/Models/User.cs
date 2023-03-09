using BankSystem.Db.Entities;

namespace BankSystem.Models
{
<<<<<<< HEAD
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? LastName { get; set; }
		public DateTime RegisteredAt { get; set; }
		public int PersonalNumber { get; set; }
		public string? Email { get; set; }
		public DateTime BirthDate { get; set; }
		public List<Account> Accounts { get; set; }
	}
=======
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string PersonalNumber { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Account> Accounts { get; set; }
    }
>>>>>>> 121230ad213c895182d809f1560e6197e43bb22b
}
