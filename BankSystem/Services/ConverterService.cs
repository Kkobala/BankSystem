using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public interface IConverterService
    {
        Task<decimal> ConvertAmountAsync(decimal amount, Currency fromCurrency, Currency toCurrency);
<<<<<<< HEAD

	}
=======
    }

>>>>>>> bdef389ba1f3f76d831491ce33d419cac69f231c
    public class ConverterService : IConverterService
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
