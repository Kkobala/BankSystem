namespace BankSystem.Models.Requests
{
    public class AddCardRequest
    {
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public DateTime CardExpirationDate { get; set; }
    }
}
