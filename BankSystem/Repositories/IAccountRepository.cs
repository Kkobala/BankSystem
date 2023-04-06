using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Requests;

namespace BankSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAsync(CreateAccountRequest request);
        Task<List<Account>> GetAccountAsync(int accountId);
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task<AccountEntity?> GetAccountByCardNumber(string cardNumber);
    }
}
