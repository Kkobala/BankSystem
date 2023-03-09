using BankSystem.Enums;

namespace BankSystem.Db.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public AccountEntity? ToAccount { get; set; }
        public AccountEntity? FromAccount { get; set; }
<<<<<<< HEAD
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
        public List<AccountEntity>? Accounts { get; set; }
        public decimal Fee { get; set; }
        public DateTime TransactionDate { get; set; }
=======
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fee { get; set; }
>>>>>>> 121230ad213c895182d809f1560e6197e43bb22b
        public TransactionType Type { get; set; }
    }
}
