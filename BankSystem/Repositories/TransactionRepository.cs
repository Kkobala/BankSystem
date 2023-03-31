using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BankSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateTransactionAsync(TransactionEntity transaction)
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task<List<TransactionEntity>> GetTransactionsAsync()
        {
            return await _db.Transactions.ToListAsync();
        }

        public async Task<List<TransactionEntity?>> GetTransactionsByCardNumber(string cardNumber)
        {
            var transaction = await _db.Transactions.Where(a => a.CardNumber == cardNumber).ToListAsync();

            if (transaction == null)
            {
                throw new ArgumentException($"Transaction with Card Number {cardNumber} Not Found");
            }

            return transaction!;
        }

        public async Task<int> CreateWithdrawAsync(TransactionEntity transaction)
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task<List<TransactionEntity>> GetAllTransactionsAsync()
        {
            var transactions = await _db.Transactions
                .Include(x => x.FromAccount)
                .Include(x => x.ToAccount)
                .Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-1) ||
                       x.TransactionDate >= DateTime.Now.AddMonths(-6) || x.TransactionDate >= DateTime.Now.AddYears(-1))
                .Where(x => x.Amount > 0)
                .ToListAsync();

            return transactions;
        }

        public async Task<ExchangeRateEntity> GetExchangeRateAsync(Currency fromCurrency, Currency toCurrency)
        {
            var exchangeRateEntity = await _db.Rates.FirstOrDefaultAsync(e => e.CurrencyFrom == fromCurrency && e.CurrencyTo == toCurrency);

			if (exchangeRateEntity == null)
            {
                throw new ArgumentException($"No exchange rate found for {fromCurrency} to {toCurrency}.");
            }

            return exchangeRateEntity;
        }
    }
}
