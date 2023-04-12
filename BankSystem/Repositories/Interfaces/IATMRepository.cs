using BankSystem.Db.Entities;
using BankSystem.Models;

namespace BankSystem.Repositories.Interfaces
{
    public interface IATMRepository
    {
        Task<CardEntity> GetCardByCardNumberAsync(string cardNumber);
    }
}
