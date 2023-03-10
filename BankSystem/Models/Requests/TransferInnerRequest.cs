namespace BankSystem.Models.Requests
{
	public class TransferInnerRequest
	{
		public int AccountId { get; set; }
		public int TargetAccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
