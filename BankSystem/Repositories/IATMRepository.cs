using BankSystem.Db.Entities;
using BankSystem.Models;

namespace BankSystem.Repositories
{
	public interface IATMRepository
	{
		Task<CardEntity> GetCardByCardNumberAsync(int cardNumber);
	}
}
