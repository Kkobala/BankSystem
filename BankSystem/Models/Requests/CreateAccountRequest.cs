using BankSystem.Enums;

namespace BankSystem.Models.Requests
{
    public class CreateAccountRequest
    {
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
