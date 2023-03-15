using BankSystem.Models.Enums;

namespace BankSystem.Models.Requests
{
	public class TransferInnerRequest
	{
		public string? ToIBAN { get; set; }
        public string? FromIBAN { get; set; }
        public Currency Currency { get; set; }
		public decimal Amount { get; set; }
	}
}
