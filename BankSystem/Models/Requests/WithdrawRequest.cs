using BankSystem.Models.Enums;

namespace BankSystem.Models.Requests
{
    public class WithdrawRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        //public decimal ExchangeRate { get; set; }
    }
}
