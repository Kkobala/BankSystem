using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Requests;
using BankSystem.Repositories.Interfaces;
using BankSystem.Validations.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories.Implementations
{
	public class AccountRepository : IAccountRepository
	{
		private readonly AppDbContext _db;
		private readonly IBankSystemValidations? _validations;

		public AccountRepository(AppDbContext db)
		{
			_db = db;
		}

		public AccountRepository(
			AppDbContext db,
			IBankSystemValidations validations)
		{
			_db = db;
			_validations = validations;
		}

		public async Task<Account> CreateAsync(CreateAccountRequest request)
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

			var accounts = new Account
			{
				Id = entity.Id,
				UserId = entity.UserId,
				IBAN = entity.IBAN,
				Amount = entity.Amount,
				Currency = entity.Currency
			};

			return accounts;
		}

		public async Task<List<Account>> GetAccountAsync(string userId)
		{
			var entity = await _db.Accounts.Where(a => a.UserId.ToString() == userId).ToListAsync();

			if (entity == null)
			{
				throw new ArgumentException($"Account with this {userId} cannot be found");
			}

			var accounts = entity.Select(e => new Account
			{
				Id = e.Id,
				UserId = e.UserId,
				IBAN = e.IBAN,
				Amount = e.Amount,
				Currency = e.Currency
			}).ToList();

			return accounts;
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
