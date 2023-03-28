using BankSystem.Models.Requests;
using BankSystem.Models;
using BankSystem.Db.Entities;

namespace BankSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(CreateAccountRequest request);
        Task<Account?> GetAccountAsync(int accountId);
        Task<AccountEntity?> GetAccountById(int accountid);
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task UpdateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByCardNumber(string cardNumber);
    }
}
