using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Enums;
using BankSystem.Models.Requests;

namespace BankSystem.Repositories
{
    public interface ICardRepository
    {
        Task<Card> AddCardAsync(AddCardRequest request);
        Task<CardEntity> ChangePINAsync(ChangePINRequest request);
        Task<List<Card>> GetUserCardsAsync(string userId);
        Task<CardEntity?> GetCardByPIN(int pin);
    }
}
