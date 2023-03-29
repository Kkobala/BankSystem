using BankSystem.Db;
using BankSystem.Db.Entities;
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

        public async Task AddCardAsync(AddCardRequest request)
        {
            var account = await _db.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.AccountId)
                ?? throw new Exception("Can not find account");

            var card = new CardEntity()
            {
                AccountId = request.AccountId,
                CardNumber = request.CardNumber,
                Account = account,
                PIN = request.PIN,
                CVV = request.CVV,
                CardExpirationDate = request.CardExpirationDate
            };

            await _db.Cards.AddAsync(card);

            _validation.CheckCardNumberFormat(request.CardNumber);
            _validation.PinValidation(request.PIN);
            _validation.CvvValidation(request.CVV);

            await _db.SaveChangesAsync();
        }

        public async Task<CardEntity> ChangePINAsync(ChangePINRequest request)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (card == null)
            {
                throw new ArgumentException("Can not find a Card");
            }

            card.PIN = request.NewPIN;

            _db.Cards.Update(card);

            await _db.SaveChangesAsync();

            return card;
        }

        public async Task<List<CardEntity>> GetUserCardsAsync(int accountId)
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

            await _db.SaveChangesAsync();

            return account.ToList();
        }

        public async Task UpdateCardAsync(CardEntity card)
        {
            _db.Cards.Update(card);
            await _db.SaveChangesAsync();
        }

        public async Task<CardEntity?> GetCardByPIN(int pin)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(a => a.PIN == pin);

            return card;
        }

        public async Task<CardEntity?> GetCardById(int id)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(a => a.Id == id);

            if (card == null)
            {
                throw new Exception($"Card with ID {id} not found");
            }

            return card;
        }
    }
}
