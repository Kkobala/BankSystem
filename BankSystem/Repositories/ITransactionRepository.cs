using BankSystem.Db.Entities;

namespace BankSystem.Repositories
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(TransactionEntity transaction);
        Task<List<TransactionEntity>> GetTransactionsAsync();
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task<AccountEntity?> GetAccountById(int id);

        Task<int> CreateWithdrawAsync(TransactionEntity transaction);
    }
}
