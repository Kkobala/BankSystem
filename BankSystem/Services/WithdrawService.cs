using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public class WithdrawService
    {
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<int> Withdraw(int accountId, decimal amount, Currency currency, decimal exchangeRate)
        {
            var account = await _transactionRepository.GetAccountById(accountId);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            if (account.Amount < amount)
            {
                throw new Exception("Insufficient balance.");
            }

            var transaction = new TransactionEntity
            {
                AccountId = account.Id,
                Amount = amount,
                Currency = currency,
                Fee = 0,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.ATM
            };

            var fee = CalculateFee(amount, currency);

            transaction.Fee = fee;
            transaction.Amount -= fee;
            transaction.Amount /= exchangeRate;

            account.Amount -= amount;

            await _transactionRepository.CreateWithdrawAsync(transaction);

            return transaction.Id;
        }

        private decimal CalculateFee(decimal amount, Currency currency)
        {
            decimal fee = 0;

            if (currency == Currency.GEL)
            {
                fee = amount * 0.00m;
            }
            else if (currency == Currency.EUR)
            {
                fee = amount * 0.01m;
            }
            else if (currency == Currency.USD)
            {
                fee = amount * 0.02m;
            }

            return fee;
        }
    }
}
