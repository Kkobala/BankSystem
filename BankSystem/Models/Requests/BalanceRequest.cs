namespace BankSystem.Models.Requests
{
	public class BalanceRequest
	{
		public bool Success { get; set; }
		public string CardNumber { get; set; }
		public Dictionary<string, decimal> Balance { get; set; } = new Dictionary<string, decimal>();
		public string ErrorMessage { get; set; }
	}
}
