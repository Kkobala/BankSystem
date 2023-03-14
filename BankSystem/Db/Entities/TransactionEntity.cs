using BankSystem.Models.Enums;

namespace BankSystem.Db.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public AccountEntity? ToIBAN { get; set; }
        public AccountEntity? FromIBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public List<AccountEntity>? Accounts { get; set; }
        public decimal Fee { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }

        public TransactionEntity()
        {
            Accounts = new List<AccountEntity>();
        }
    }
}
