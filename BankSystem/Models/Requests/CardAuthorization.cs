namespace BankSystem.Models.Requests
{
	public class CardAuthorizationRequest
	{
		public int CardNumber { get; set; }
		public int PinCode { get; set; }
	}
}
