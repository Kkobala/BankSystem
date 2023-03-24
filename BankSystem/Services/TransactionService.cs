using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ConverterService _converterService;

        public TransactionService(ITransactionRepository transactionRepository,
            ConverterService converterService)
        {
            _transactionRepository = transactionRepository;
            _converterService = converterService;
        }

        public async Task<int> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency)
        {
            var fromiban = await _transactionRepository.GetAccountByIBAN(fromIBAN);
            var toiban = await _transactionRepository.GetAccountByIBAN(toIBAN);

            if (fromiban == null || toiban == null)
            {
                throw new Exception("One or more account(s) not found");
            }

            decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, fromiban.Currency, toCurrency);

            if (fromiban.Amount < convertedAmount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = convertedAmount * 0.00m;

            var transaction = new TransactionEntity
            {
                FromIBAN = fromiban,
                ToIBAN = toiban,
                Amount = convertedAmount,
                Currency = toCurrency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Inner
            };

            fromiban.Amount -= convertedAmount + fee;

            if (toiban.Transactions == null)
            {
                toiban.Transactions = new List<TransactionEntity>();
            }

            fromiban.Amount -= convertedAmount + fee;
            toiban.Amount += convertedAmount;

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

            decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, currency, fromiban.Currency);

            var fee = (convertedAmount * 0.01m) + 0.5m;

            var transaction = new TransactionEntity
            {
                FromIBAN = fromiban,
                ToIBAN = toiban,
                Amount = convertedAmount,
                Currency = fromiban.Currency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Outter
            };

            fromiban.Amount -= convertedAmount + fee;

            if (toiban.Transactions == null)
            {
                toiban.Transactions = new List<TransactionEntity>();
            }

            toiban.Amount += convertedAmount;

            toiban.Transactions.Add(transaction);

            await _transactionRepository.CreateTransactionAsync(transaction);

            return transaction.Id;
        }
    }
}
