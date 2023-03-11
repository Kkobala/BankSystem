using BankSystem.Db.Entities;

namespace BankSystem.Repositories
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(TransactionEntity transaction);
        Task<List<TransactionEntity>> GetTransactionsAsync();
        Task<AccountEntity> GetAccountById(int id);
    }
}
