﻿using BankSystem.Models;
using BankSystem.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Db.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public List<CardEntity> Cards { get; set; }

        [NotMapped]
        public List<TransactionEntity> Transactions { get; set; }

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
                    OwnerFullName = cardEntity.OwnerFullName,
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
