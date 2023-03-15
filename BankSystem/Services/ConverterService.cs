using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Enums;
using BankSystem.Repositories;

namespace BankSystem.Services
{
    public class ConverterService
    {
        public decimal ConvertAmount(decimal amount, Currency fromCurrency, Currency toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return amount;
            }

            decimal exchangeRate = 0;

            switch (fromCurrency)
            {
                case Currency.GEL:
                    exchangeRate = toCurrency switch
                    {
                        Currency.USD => (decimal)ExchangeRates.GEL_TO_USD,
                        Currency.EUR => (decimal)ExchangeRates.GEL_TO_EUR,
                        _ => throw new NotImplementedException($"Conversion from {fromCurrency} to {toCurrency} is not supported.")
                    };
                    break;

                case Currency.USD:
                    exchangeRate = toCurrency switch
                    {
                        Currency.GEL => (decimal)ExchangeRates.USD_TO_GEL,
                        _ => throw new NotImplementedException($"Conversion from {fromCurrency} to {toCurrency} is not supported.")
                    };
                    break;

                case Currency.EUR:
                    exchangeRate = toCurrency switch
                    {
                        Currency.GEL => (decimal)ExchangeRates.EUR_TO_GEL,
                        _ => throw new NotImplementedException($"Conversion from {fromCurrency} to {toCurrency} is not supported.")
                    };
                    break;

                default:
                    throw new NotImplementedException($"Conversion from {fromCurrency} to {toCurrency} is not supported.");
            }

            decimal convertedAmount = amount * exchangeRate;

            return convertedAmount;
        }
    }
}
