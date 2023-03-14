using BankSystem.Db.Entities;
using BankSystem.Db;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateTransactionAsync(TransactionEntity transaction)
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task<List<TransactionEntity>> GetTransactionsAsync()
        {
            return await _db.Transactions.ToListAsync();
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

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            return await _db.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> CreateWithdrawAsync(TransactionEntity transaction)
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return transaction.Id;
        }
    }
}
