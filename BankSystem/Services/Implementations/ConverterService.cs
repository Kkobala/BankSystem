using BankSystem.Models.Enums;
using BankSystem.Repositories.Interfaces;
using BankSystem.Services.Interfaces;
using BankSystem.UnitofWork;

namespace BankSystem.Services.Implementations
{
    public class ConverterService : IConverterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConverterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> ConvertAmountAsync(decimal amount, Currency fromCurrency, Currency toCurrency)
        {
            var exchangeRate = await _unitOfWork.TransactionRepository.GetExchangeRateAsync(fromCurrency, toCurrency);

            var convertedAmount = amount * exchangeRate.Rate;

            return convertedAmount;
        }
    }
}
