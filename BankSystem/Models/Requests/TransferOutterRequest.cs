using BankSystem.Models.Enums;

namespace BankSystem.Models.Requests
{
	public class TransferOutterRequest
	{
        public int ToAccountId { get; set; }
        public int FromAccountId { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
