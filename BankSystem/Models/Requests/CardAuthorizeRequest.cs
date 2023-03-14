namespace BankSystem.Models.Requests
{
	public class CardAuthorizeRequest
	{
		public bool Success { get; set; }
		public decimal Balance { get; set; }
		public string ErrorMessage { get; set; }
		//public int CardNumber { get; set; }
		//public int PinCode { get; set; }

		public static CardAuthorizeRequest CardNotFound => new CardAuthorizeRequest { Success = false, ErrorMessage = "Card not found" };
		public static CardAuthorizeRequest InvalidPIN => new CardAuthorizeRequest { Success = false, ErrorMessage = "Invalid PIN" };
		public static CardAuthorizeRequest CardExpired => new CardAuthorizeRequest { Success = false, ErrorMessage = "Card has expired" };
	}
}
