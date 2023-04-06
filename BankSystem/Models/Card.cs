namespace BankSystem.Models
{
	public class Card
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public string? CardNumber { get; set; }
		public DateTime CardExpirationDate { get; set; }
		public int CVV { get; set; }
		public int PIN { get; set; }
	}
}
