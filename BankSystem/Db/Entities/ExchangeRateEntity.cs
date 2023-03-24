using BankSystem.Models.Enums;

namespace BankSystem.Db.Entities
{
    public class ExchangeRateEntity
    {
        public int Id { get; set; }
        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        public decimal Rate { get; set; }
    }
}
