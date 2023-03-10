using BankSystem.Db.Entities;
using BankSystem.Db;
using BankSystem.Models.Requests;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;
        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateAccountRequest request)
        {
            AccountEntity entity = new AccountEntity();
            entity.UserId = request.UserId;
            entity.IBAN = request.IBAN;
            entity.Amount = request.Amount;
            entity.Currency = request.Currency;
            await _db.Accounts.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Account?> GetAccountAsync(int userId)
        {
            var entity = await _db.Accounts.Include(a => a.Cards).FirstOrDefaultAsync(a => a.UserId == userId);

            if (entity == null)
            {
                return null;
            }

            return entity.ToDomainModel();
        }
    }
}
