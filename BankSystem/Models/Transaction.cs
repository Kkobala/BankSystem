using BankSystem.Db.Entities;
using BankSystem.Enums;

namespace BankSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public AccountEntity ToAccount { get; set; }
        public AccountEntity FromAccount { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public List<CardEntity> Cards { get; set; }
        public TransactionType Type { get; set; }
    }
}
