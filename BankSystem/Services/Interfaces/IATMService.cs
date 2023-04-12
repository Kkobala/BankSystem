using BankSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Interfaces
{
	public interface IATMService
	{
		Task<(bool, string)> AuthorizeCardAsync(string cardNumber, int pinCode);
		Task<decimal> GetBalanceAsync(string cardNumber, int pin);
		Task<decimal> Withdraw(string cardNumber, int pin, decimal amount, Currency toCurrency);
	}
}
