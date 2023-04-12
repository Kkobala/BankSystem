using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Interfaces
{
	public interface ITransactionService
	{
		Task<decimal> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount);
		Task<decimal> OutTransactionAsync(string fromIBAN, string toIBAN, decimal amount);
	}
}
