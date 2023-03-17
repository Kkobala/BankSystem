using BankSystem.Db.Entities;

namespace BankSystem.Models.Requests
{
    public class AddCardRequest
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public DateTime CardExpirationDate { get; set; }
    }
}
