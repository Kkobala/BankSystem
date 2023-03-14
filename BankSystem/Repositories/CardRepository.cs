using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
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
                CardExpirationDate = request.CardExpirationDate,
                OwnerName = request.OwnerName,
                OwnerLastName = request.OwnerLastName
            };

            //await _db.Accounts.AddAsync(account);
            await _db.Cards.AddAsync(card);
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

        //public async Task<TransactionEntity> CashOutAsync(int accountId, decimal amount, Currency currency, decimal exchangeRate)
        //{
        //    var account = await _db.Accounts.FindAsync(accountId);

        //    //var transaction = new TransactionEntity
        //    //{
        //    //    AccountId = account.Id,
        //    //    Amount = amount,
        //    //    Currency = currency,
        //    //    Fee = 0,
        //    //    TransactionDate = DateTime.UtcNow,
        //    //    Type = TransactionType.ATM
        //    //};

        //    //var fee = CalculateFee(amount, currency);

        //    //transaction.Fee = fee;
        //    //transaction.Amount -= fee;
        //    //transaction.Amount /= exchangeRate;

        //    //account.Amount -= amount;

        //    await _db.Transactions.AddAsync(transaction);
        //    await _db.SaveChangesAsync();

        //    return transaction;
        //}

        ////private decimal CalculateFee(decimal amount, Currency currency)
        ////{
        ////    decimal fee = 0;

        ////    if (currency == Currency.GEL)
        ////    {
        ////        fee = amount * 0.00m;
        ////    }
        ////    else if (currency == Currency.EUR)
        ////    {
        ////        fee = amount * 0.01m;
        ////    }
        ////    else if (currency == Currency.USD)
        ////    {
        ////        fee = amount * 0.02m;
        ////    }

        ////    return fee;
        ////}


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
    }
}
