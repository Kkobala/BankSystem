using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public interface ITransactionService
    {
        Task<decimal> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency);
        Task<decimal> OutTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConverterService _converterService;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IConverterService converterService,
            IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _converterService = converterService;
            _accountRepository = accountRepository;
        }

        public async Task<decimal> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency)
        {
            var fromiban = await _accountRepository.GetAccountByIBAN(fromIBAN);
            var toiban = await _accountRepository.GetAccountByIBAN(toIBAN);

            ValidateAccountWithIBAN(fromiban, toiban);
            CheckSenderAndRecieverId(fromiban, toiban);
            CheckAmount(amount);

            decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, fromiban.Currency, toCurrency);

            if (fromiban.Amount < convertedAmount)
            {
                throw new Exception("Insufficient funds");
            }

            var fee = convertedAmount * 0.00m;

            var transaction = new TransactionEntity
            {
                FromAccount = fromiban,
                ToAccount = toiban,
                Amount = convertedAmount,
                Currency = toCurrency,
                Fee = fee,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Inner
            };

            fromiban.Amount -= convertedAmount + fee;
            toiban.Amount += convertedAmount;

            if (toiban.Transactions == null)
            {
                toiban.Transactions = new List<TransactionEntity>();
            }

            await _transactionRepository.CreateTransactionAsync(transaction);

            return toiban.Amount;
        }

        public async Task<decimal> OutTransactionAsync(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency)
        {
            var fromiban = await _accountRepository.GetAccountByIBAN(fromIBAN);
            var toiban = await _accountRepository.GetAccountByIBAN(toIBAN);

            ValidateAccountWithIBAN(fromiban, toiban);

            CheckUsersIdNotBeSame(fromiban, toiban);

            CheckAmount(amount);

            CheckSenderBalance(amount, fromiban);

            decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, fromiban.Currency, toCurrency);

            var fee = (convertedAmount * 0.01m) + 0.5m;

            var transaction = new TransactionEntity
            {
                FromAccount = fromiban,
                ToAccount = toiban,
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

            await _transactionRepository.CreateTransactionAsync(transaction);

            return toiban.Amount;
        }

        private static void CheckSenderBalance(decimal amount, AccountEntity fromiban)
        {
            if (fromiban.Amount < amount)
            {
                throw new Exception("Insufficient funds");
            }
        }

        private static void CheckUsersIdNotBeSame(AccountEntity fromiban, AccountEntity toiban)
        {
            if (fromiban.UserId == toiban.UserId)
            {
                throw new Exception("Sender and receiver must not be the same user");
            }
        }

        private static void CheckAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
        }

        private static void CheckSenderAndRecieverId(AccountEntity fromiban, AccountEntity toiban)
        {
            if (fromiban.UserId != toiban.UserId)
            {
                throw new Exception("Sender and receiver must be the same user");
            }
        }

        private static void ValidateAccountWithIBAN(AccountEntity fromiban, AccountEntity toiban)
        {
            if (fromiban == null || toiban == null)
            {
                throw new Exception("One or more account(s) not found");
            }
        }
    }
}
