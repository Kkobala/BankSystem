using BankSystem.Db.Entities;
using BankSystem.Models.Enums;

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

        public Transaction()
        {
            Cards = new List<Card>();
        }
    }
}
