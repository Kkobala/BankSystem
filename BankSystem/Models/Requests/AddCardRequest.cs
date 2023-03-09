using BankSystem.Db.Entities;

namespace BankSystem.Models.Requests
{
    public class AddCardRequest
    {
        public int AccountId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public int CardNumber { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public List<AccountEntity> Accounts { get; set; }
    }
}
