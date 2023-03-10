using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _db;

        public CardRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddCardAsync(AddCardRequest request)
        {
            var card = new CardEntity()
            {
                AccountId = request.AccountId,
                CardNumber = request.CardNumber,
                PIN = request.PIN,
                CVV = request.CVV,
                CardExpirationDate = request.CardExpirationDate,
                OwnerName = request.OwnerName,
                OwnerLastName = request.OwnerLastName
            };

            await _db.Cards.AddAsync(card);
            await _db.SaveChangesAsync();
        }

        public async Task<CardEntity> ChangePINAsync(ChangePINRequest request)
        {
            var changedPIN = await _db.Cards.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (changedPIN == null)
            {
                throw new ArgumentException("Can not find a Card");
            }

            changedPIN.PIN = request.PIN;

            _db.Cards.Update(changedPIN);

            await _db.SaveChangesAsync();

            return changedPIN;
        }

        public async Task<List<CardEntity>> GetUserCardsAsync(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            List<CardEntity> userCards = new List<CardEntity>();

            foreach (var card in userCards)
            {
                if (card.CardExpirationDate < DateTime.Now)
                {
                    throw new Exception("Your card will expired");
                }
                else
                if (card.CardExpirationDate < DateTime.Now.AddMonths(3))
                {
                    throw new Exception("Your card will expire in 3 months");
                }
            }

            await _db.SaveChangesAsync();

            return userCards;
        }
    }
}
