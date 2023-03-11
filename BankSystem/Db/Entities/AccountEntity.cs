using BankSystem.Models;
using BankSystem.Models.Enums;
using IbanNet;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Db.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "IBAN is required.")]
        [StringLength(34, MinimumLength = 15, ErrorMessage = "IBAN length should be between 15 and 34.")]
        [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z\d]{1,30}$", ErrorMessage = "Invalid IBAN format.")]
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string? Json { get; set; }
        public List<CardEntity> Cards { get; set; }
        [NotMapped]
        public List<TransactionEntity> Transactions { get; internal set; }

        public AccountEntity()
        {
            Cards = new List<CardEntity>();
            Transactions = new List<TransactionEntity>();
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

        public bool IsValidIBAN()
        {
            IIbanValidator validator = new IbanValidator();
            return validator.Validate(IBAN).IsValid;
        }
    }
}
