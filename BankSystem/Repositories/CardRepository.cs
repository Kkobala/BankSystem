using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Requests;
using BankSystem.Validations;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _db;
        private readonly BankSystemValidations _validation;

        public CardRepository(
            AppDbContext db,
            BankSystemValidations validations)
        {
            _db = db;
            _validation = validations;
        }

        public async Task<Card> AddCardAsync(AddCardRequest request)
        {
            var account = await _db.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.AccountId)
                ?? throw new Exception($"Account with {request.AccountId} cannot be found");

            var cardEntity = new CardEntity()
            {
                AccountId = request.AccountId,
                CardNumber = request.CardNumber,
                Account = account,
                PIN = request.PIN,
                CVV = request.CVV,
                CardExpirationDate = request.CardExpirationDate
            };

            _validation.CheckCardNumberFormat(request.CardNumber);
            _validation.PinValidation(request.PIN);
            _validation.CvvValidation(request.CVV);

            await _db.Cards.AddAsync(cardEntity);

            await _db.SaveChangesAsync();

            var card = new Card()
            {
                Id = cardEntity.Id,
                AccountId = cardEntity.AccountId,
                CardNumber = cardEntity.CardNumber,
                CVV = cardEntity.CVV,
                PIN = cardEntity.PIN,
                CardExpirationDate = cardEntity.CardExpirationDate
            };

            return card;
        }

        public async Task<CardEntity> ChangePINAsync(ChangePINRequest request)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (card == null)
            {
                throw new ArgumentException($"Card with {request.Id} cannot be found");
            }

            card.PIN = request.NewPIN;

            _db.Cards.Update(card);

            await _db.SaveChangesAsync();

            return card;
        }

        public async Task<List<Card>> GetUserCardsAsync(int accountId)
        {
            var account = await _db.Cards
                .Where(u => u.AccountId == accountId)
                .ToListAsync();

            Parallel.ForEach(account, acc =>
            {
                if (acc.CardExpirationDate < DateTime.Now)
                {
                    throw new Exception("Your card will expired");
                }
                else if (acc.CardExpirationDate < DateTime.Now.AddMonths(3))
                {
                    throw new Exception("Your card will expire in 3 months");
                }
            });

            var card = account.Select(e => new Card
            {
                Id = e.Id,
                AccountId = e.AccountId,
                CardNumber = e.CardNumber,
                CardExpirationDate = e.CardExpirationDate,
                CVV = e.CVV,
                PIN = e.PIN,
            }).ToList();

            await _db.SaveChangesAsync();

            return card;
        }

        public async Task<CardEntity?> GetCardByPIN(int pin)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(a => a.PIN == pin);

            return card;
        }
    }
}
