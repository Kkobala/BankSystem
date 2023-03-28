using BankSystem.Models.Enums;

namespace BankSystem.Models.Requests
{
    public class WithdrawRequest
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
