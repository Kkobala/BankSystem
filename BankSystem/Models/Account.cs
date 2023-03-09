using BankSystem.Enums;

namespace BankSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public List<Card> Cards { get; set; }
    }
}
