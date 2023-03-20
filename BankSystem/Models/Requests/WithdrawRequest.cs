using BankSystem.Models.Enums;

namespace BankSystem.Models.Requests
{
    public class WithdrawRequest
    {
        public int AccountId { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
