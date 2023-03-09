using BankSystem.Enums;
using BankSystem.Models;
using Newtonsoft.Json;

namespace BankSystem.Db.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string? Json { get; set; }

        public List<CardEntity>? Cards { get; set; }

        public static AccountEntity FromDomainModel(Account account)
        {
            return new AccountEntity
            {
                Json = JsonConvert.SerializeObject(account.Id)
            };
        }
		public Account ToDomainModel()
		{
			var cards = new List<Card>();

			foreach (var cardEntity in Cards)
			{
				cards.Add(new Card
				{
					Id = cardEntity.Id,
					CardNumber = cardEntity.CardNumber,
					OwnerName = cardEntity.OwnerName,
					OwnerLastName = cardEntity.OwnerLastName,
					CardExpirationDate = cardEntity.CardExpirationDate,
					CVV = cardEntity.CVV,
					PIN = cardEntity.PIN
				});
			}

			return new Account
			{
				Id = Id,
				UserId = UserId,
				Amount = Amount,
				Currency = Currency,
				Cards = cards
			};
		}
	}
}
