using BankSystem.Enums;

namespace BankSystem.Models
{
    public class Account
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public string IBAN { get; set; }
		public decimal Amount { get; set; }
		public Currency Currency { get; set; }

<<<<<<< HEAD
		public List<Card> Cards { get; set; }


	}
=======
        public List<Card> Cards { get; set; }
    }
>>>>>>> 121230ad213c895182d809f1560e6197e43bb22b
}
