using BankSystem.Db.Entities;
using BankSystem.Models.Enums;

namespace BankSystem.Repositories
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(TransactionEntity transaction);
        Task<decimal> GetAllTransactionsInGELAsync();
        Task<decimal> GetAllTransactionsInUSDAsync();
        Task<decimal> GetAllTransactionsInEURAsync();
        Task<int> CreateWithdrawAsync(TransactionEntity transaction);
        Task<ExchangeRateEntity> GetExchangeRateAsync(Currency fromCurrency, Currency toCurrency);
        Task<List<TransactionEntity?>> GetTransactionsByCardNumber(string cardNumber);
    }
}
