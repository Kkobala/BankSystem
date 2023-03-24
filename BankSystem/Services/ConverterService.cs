using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public class ConverterService
    {
        private readonly ITransactionRepository _transactionRepository;

        public ConverterService(ITransactionRepository repository)
        {
            _transactionRepository = repository;
        }

        public async Task<decimal> ConvertAmountAsync(decimal amount, Currency fromCurrency, Currency toCurrency)
        {
            var exchangeRate = await _transactionRepository.GetExchangeRateAsync(fromCurrency, toCurrency);

            var convertedAmount = amount * exchangeRate.Rate;

            return convertedAmount;
        }
    }
}
