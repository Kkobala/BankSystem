using BankSystem.Enums;

namespace BankSystem.Db.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public List<AccountEntity>? Accounts { get; set; }
        public List<CardEntity>? Cards { get; set; }
    }
}
