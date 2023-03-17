using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public interface IATMService
    {
        Task<(bool, string)> AuthorizeCardAsync(string cardNumber, int pinCode);
        Task<decimal> GetBalanceAsync(string cardNumber);
        Task<int> Withdraw(int accountId, decimal amount, Currency fromCurrency, Currency toCurrency);
    }

    public class ATMService : IATMService
    {
        private readonly IATMRepository _repository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ConverterService _converterService;

        public ATMService(IATMRepository repository,
            ITransactionRepository transactionRepository,
            ConverterService converterService)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            _converterService = converterService;
        }

        public async Task<int> Withdraw(int accountId, decimal amount, Currency fromCurrency, Currency toCurrency)
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

            var fee = CalculateFee(amount, fromCurrency);

            var transaction = new TransactionEntity
            {
                AccountId = account.Id,
                Amount = amount,
                Currency = toCurrency,
                Fee = 0,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.ATM
            };

            transaction.Fee = fee;
            transaction.Amount -= fee;

            var usdAmount = _converterService.ConvertAmount(amount, fromCurrency, Currency.USD);

            var convertedAmount = _converterService.ConvertAmount(usdAmount, Currency.USD, toCurrency);

            transaction.Amount = convertedAmount;

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
            else if (currency == Currency.EUR || currency == Currency.USD)
            {
                fee = (amount * 0.01m) + 0.5m;
            }

            return fee;
        }

        public async Task<(bool, string)> AuthorizeCardAsync(string cardNumber, int pin)
        {
            var card = await _repository.GetCardByCardNumberAsync(cardNumber);
            if (card == null)
            {
                return (false, "Invalid card number.");
            }

            if (card.CardExpirationDate < DateTime.UtcNow)
            {
                return (false, "Card has expired.");
            }

            if (card.PIN != pin)
            {
                return (false, "Incorrect PIN.");
            }

            return (true, "Card authorized successfully.");
        }

        public async Task<decimal> GetBalanceAsync(string cardNumber)
        {
            var card = await _repository.GetCardByCardNumberAsync(cardNumber);
            return card.Balance;
        }
    }
}
