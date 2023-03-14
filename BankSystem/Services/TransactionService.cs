using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<int> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount)
        {
            var fromiban = await _transactionRepository.GetAccountByIBAN(fromIBAN);
            var toiban = await _transactionRepository.GetAccountByIBAN(toIBAN);

            if (fromiban == null || toiban == null)
            {
                throw new Exception("One or more account(s) not found");
            }

            if (fromiban.Amount < amount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = amount * 0.00m; // assuming fee is 1% of the amount

            var transaction = new TransactionEntity
            {
                FromIBAN = fromiban,
                ToIBAN = toiban,
                Amount = amount,
                Currency = fromiban.Currency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Inner
            };

            fromiban.Amount -= amount + fee;
            toiban.Amount += amount;

            await _transactionRepository.CreateTransactionAsync(transaction);

            return transaction.Id;
        }

        public async Task<int> OutTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency currency)
        {
            var fromiban = await _transactionRepository.GetAccountByIBAN(fromIBAN);
            var toiban = await _transactionRepository.GetAccountByIBAN(toIBAN);

            if (fromiban == null || toiban == null)
            {
                throw new Exception("One or more account(s) not found");
            }

            if (fromiban.Amount < amount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = (amount * 0.01m) + 0.5m; // assuming fee is 1% of the amount

            var transaction = new TransactionEntity
            {
                FromIBAN = fromiban,
                ToIBAN = toiban,
                Amount = amount,
                Currency = currency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Outter
            };

            fromiban.Amount -= amount + fee;

            // add transaction to the list of transactions of the 'toAccount'
            if (toiban.Transactions == null)
            {
                toiban.Transactions = new List<TransactionEntity>();
            }

            toiban.Transactions.Add(transaction);

            await _transactionRepository.CreateTransactionAsync(transaction);

            return transaction.Id;
        }
    }
}
