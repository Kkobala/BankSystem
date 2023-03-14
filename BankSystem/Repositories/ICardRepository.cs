using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Models.Requests;

namespace BankSystem.Repositories
{
    public interface ICardRepository
    {
        Task AddCardAsync(AddCardRequest request);
        Task<CardEntity> ChangePINAsync(ChangePINRequest request);
        Task<List<CardEntity>> GetUserCardsAsync(int userId);
        //Task<TransactionEntity> CashOutAsync(int accountId, decimal amount, Currency currency, decimal exchangeRate);
    }
}
