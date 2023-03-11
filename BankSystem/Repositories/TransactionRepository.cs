using BankSystem.Db.Entities;
using BankSystem.Db;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateTransactionAsync(TransactionEntity transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task<List<TransactionEntity>> GetTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<AccountEntity> GetAccountById(int id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }
    }
}
