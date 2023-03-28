namespace BankSystem.Models
{
	public class Card
	{
		public int Id { get; set; }
		public decimal Balance { get; set; }
		public string? CardNumber { get; set; }
		public string? OwnerFullName { get; set; }
		public DateTime CardExpirationDate { get; set; }
		public int CVV { get; set; }
		public int PIN { get; set; }
	}
}
