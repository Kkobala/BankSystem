using BankSystem.Enums;

namespace BankSystem.Db.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public AccountEntity? ToAccount { get; set; }
        public AccountEntity? FromAccount { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public List<CardEntity>? Cards { get; set; }
        public TransactionType Type { get; set; }
    }
}
