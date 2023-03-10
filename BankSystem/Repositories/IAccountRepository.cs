using BankSystem.Models.Requests;
using BankSystem.Models;

namespace BankSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(CreateAccountRequest request);
        Task<Account?> GetAccountAsync(int accountId);
    }
}
