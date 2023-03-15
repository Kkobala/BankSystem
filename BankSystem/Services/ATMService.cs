using BankSystem.Db;
using BankSystem.Models;
using BankSystem.Models.Requests;
using BankSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Services
{
	public interface IATMService
	{
		Task<(bool,string)> AuthorizeCardAsync(string cardNumber, int pinCode);
		Task<decimal> GetBalanceAsync(string cardNumber);
	}
	public class ATMService : IATMService
	{
		private readonly IATMRepository _repository;
		public ATMService(IATMRepository repository)
		{
			_repository = repository;
		}


		public async Task<(bool, string)> AuthorizeCardAsync(string cardNumber, int pin)
		{
			var card = await _repository.GetCardByCardNumberAsync(cardNumber);
			if (card == null)
			{
				return (false, "Invalid card number.");
			}

			if (card.CardExpirationDate < DateTime.UtcNow)
			{
				return (false, "Card has expired.");
			}

			if (card.PIN != pin)
			{
				return (false, "Incorrect PIN.");
			}

			return (true, "Card authorized successfully.");
		}

		public async Task<decimal> GetBalanceAsync(string cardNumber)
		{
			var card = await _repository.GetCardByCardNumberAsync(cardNumber);
			return card.Balance;
		}

	}
}
		
		
			
