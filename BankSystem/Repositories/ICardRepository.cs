using BankSystem.Db.Entities;
using BankSystem.Models.Requests;

namespace BankSystem.Repositories
{
    public interface ICardRepository
    {
        Task AddCardAsync(AddCardRequest request);
        Task<CardEntity> ChangePINAsync(ChangePINRequest request);
        Task<List<CardEntity>> GetUserCardsAsync(int userId);
    }
}
