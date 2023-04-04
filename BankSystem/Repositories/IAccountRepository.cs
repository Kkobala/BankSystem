using BankSystem.Models.Requests;
using BankSystem.Models;
using BankSystem.Db.Entities;

namespace BankSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAsync(CreateAccountRequest request);
        Task<List<AccountEntity>> GetAccountAsync(int accountId);
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task UpdateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByCardNumber(string cardNumber);
	}
}
