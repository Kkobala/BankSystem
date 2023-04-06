using BankSystem.Db.Entities;
using BankSystem.Db;
using BankSystem.Models.Requests;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using BankSystem.Validations;
using IbanNet;

namespace BankSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;
        private readonly BankSystemValidations? _validations;
        
        public AccountRepository(AppDbContext db)
        {
            _db= db;
        }
        
        public AccountRepository(
            AppDbContext db,
            BankSystemValidations validations)
        {
            _db = db;
            _validations = validations;
        }

        public async Task<AccountEntity> CreateAsync(CreateAccountRequest request)
        {
			var checkIban = await _db.Accounts.AnyAsync(x => x.IBAN == request.IBAN);

            if (checkIban)
            {
                throw new ArgumentException("Iban already registered");
            }

			AccountEntity entity = new AccountEntity();
            entity.UserId = request.UserId;
            entity.IBAN = request.IBAN;
            entity.Amount = request.Amount;
            entity.Currency = request.Currency;

            _validations!.CheckIbanFormat(request.IBAN);

            await _db.Accounts.AddAsync(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<List<AccountEntity>> GetAccountAsync(int userId)
        {
            var entity = await _db.Accounts.Where(a => a.UserId == userId).ToListAsync();

            if (entity == null)
            {
                throw new ArgumentException($"Account with this {userId} cannot be found");
            }

            return entity.ToList();
        }

        public async Task<AccountEntity> GetAccountByIBAN(string iban)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.IBAN == iban);

            if (account == null)
            {
                throw new Exception($"Account with IBAN {iban} not found");
            }

            return account;
        }

        public async Task<AccountEntity?> GetAccountByCardNumber(string cardNumber)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Cards.Any(c => c.CardNumber == cardNumber));

            return account;
        }
    }
}
