using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Db.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string CardNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public AccountEntity Account { get; set; }
    }
}
