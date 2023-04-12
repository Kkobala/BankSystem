using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Requests;

namespace BankSystem.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(CreateAccountRequest request);
        Task<List<Account>> GetAccountAsync(string userId);
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task<AccountEntity?> GetAccountByCardNumber(string cardNumber);
    }
}
