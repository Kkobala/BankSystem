using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<TransactionEntity>> GetTransactionsByAccountId(int accoundId)
        {
            var transaction = await _db.Transactions.Where(x => x.AccountId == accoundId).ToListAsync();

            if (transaction == null)
            {
                throw new ArgumentException($"Transaction with Accound ID {accoundId} Not Found");
            }

            return transaction;
        }

        public async Task<AccountEntity> GetAccountByIBAN(string iban)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.IBAN == iban);

            if (account == null)
            {
                throw new Exception($"Account with IBAN {iban} not found");
            }

            return account;
        }

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found");
            }

            return account;
        }

        public async Task<CardEntity?> GetCardById(int id)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(a => a.Id == id);

            if (card == null)
            {
                throw new Exception($"Card with ID {id} not found");
            }

            return card;
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
                .Include(x => x.FromIBAN)
                .Include(x => x.ToIBAN)
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

        public async Task UpdateCardAsync(CardEntity card)
        {
            _db.Cards.Update(card);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(AccountEntity account)
        {
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }
    }
}
