using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BankSystem.Repositories
{
	public interface IAccountRepository
	{
		Task<int> CreateAsync(CreateAccountRequest request);
		Task<Account?> GetAccountAsync(int accountId);
	}
	public class AccountRepository : IAccountRepository
	{
		private readonly AppDbContext _db;
		public AccountRepository(AppDbContext db)
		{
			_db = db;
		}

		public async Task<int> CreateAsync(CreateAccountRequest request)
		{
			AccountEntity entity= new AccountEntity();
			entity.UserId = request.UserId;
			entity.IBAN= request.IBAN;
			entity.Amount= request.Amount;
			entity.Currency= request.Currency;
			await _db.Accounts.AddAsync(entity);
			await _db.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<Account?> GetAccountAsync(int accountId)
		{
			var entity = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
			if (entity == null)
			{
				return null;
			}

			return entity.ToDomainModel();
		}
	}
}
