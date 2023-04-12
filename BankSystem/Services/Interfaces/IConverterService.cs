using BankSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Interfaces
{
	public interface IConverterService
	{
		Task<decimal> ConvertAmountAsync(decimal amount, Currency fromCurrency, Currency toCurrency);
	}
}
