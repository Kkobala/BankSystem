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

        public async Task<int> InnerTransactionAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = await _transactionRepository.GetAccountById(fromAccountId);
            var toAccount = await _transactionRepository.GetAccountById(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                throw new Exception("One or more account(s) not found");
            }

            if (fromAccount.Amount < amount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = amount * 0.00m; // assuming fee is 1% of the amount

            var transaction = new TransactionEntity
            {
                FromAccount = fromAccount,
                ToAccount = toAccount,
                Amount = amount,
                Currency = fromAccount.Currency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Inner
            };

            fromAccount.Amount -= amount + fee;
            toAccount.Amount += amount;

            await _transactionRepository.CreateTransactionAsync(transaction);

            return transaction.Id;
        }

        public async Task<int> OutTransactionAsync(int fromAccountId, int toAccountId, decimal amount, Currency currency)
        {
            var fromAccount = await _transactionRepository.GetAccountById(fromAccountId);
            var toAccount = await _transactionRepository.GetAccountById(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                throw new Exception("One or more account(s) not found");
            }

            if (fromAccount.Amount < amount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = (amount * 0.01m) + 0.5m; // assuming fee is 1% of the amount

            var transaction = new TransactionEntity
            {
                FromAccount = fromAccount,
                ToAccount = toAccount,
                Amount = amount,
                Currency = currency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Outter
            };

            fromAccount.Amount -= amount + fee;

            // add transaction to the list of transactions of the 'toAccount'
            if (toAccount.Transactions == null)
            {
                toAccount.Transactions = new List<TransactionEntity>();
            }

            toAccount.Transactions.Add(transaction);
            await _transactionRepository.CreateTransactionAsync(transaction);

            return transaction.Id;
        }
    }
}
