<<<<<<< HEAD
﻿using BankSystem.Enums;

namespace BankSystem.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public Account ToAccount { get; set; }
		public Account FromAccount { get; set; }
		public Currency Currency { get; set; }
		public decimal Amount { get; set; }
		public List<Card> Cards { get; set; }
		public TransactionType Type { get; set; }
	}
=======
using BankSystem.Db.Entities;
using BankSystem.Enums;

namespace BankSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Account ToAccount { get; set; }
        public Account FromAccount { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public List<Card> Cards { get; set; }
        public TransactionType Type { get; set; }
    }
>>>>>>> 121230ad213c895182d809f1560e6197e43bb22b
}
