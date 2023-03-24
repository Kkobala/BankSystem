using BankSystem.Db.Entities;
using BankSystem.Models.Enums;

namespace BankSystem.Repositories
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(TransactionEntity transaction);
        Task<List<TransactionEntity>> GetTransactionsAsync();
        Task<AccountEntity> GetAccountByIBAN(string iban);
        Task<AccountEntity?> GetAccountById(int id);
        Task<List<TransactionEntity>> GetAllTransactionsAsync();
        Task<int> CreateWithdrawAsync(TransactionEntity transaction);
        Task UpdateAccountAsync(AccountEntity account);
        Task UpdateCardAsync(CardEntity card);
        Task<CardEntity?> GetCardById(int id);
        Task<ExchangeRateEntity> GetExchangeRateAsync(Currency fromCurrency, Currency toCurrency);
        Task<List<TransactionEntity>> GetTransactionsByAccountId(int accoundId);
    }
}
