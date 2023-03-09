namespace BankSystem.Models.Requests
{
	public class TransferOutterRequest
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
