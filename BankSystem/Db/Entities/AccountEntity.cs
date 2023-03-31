using BankSystem.Models;
using BankSystem.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Db.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public List<CardEntity> Cards { get; set; }

        [NotMapped]
        public List<TransactionEntity> Transactions { get; set; }

        public AccountEntity()
        {
            Cards = new List<CardEntity>();
            Transactions = new List<TransactionEntity>();
        }
    }
}
