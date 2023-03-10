using BankSystem.Db;
using BankSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BankSystem.Services
{
    public class TransactionService
    {
        private const decimal withdrawVar = 1.5M;
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext db)
        {
            _db = db;
        }

        public async Task InnerTransaction(int amount, Currency currency, string IBAN)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(a => a.IBAN == IBAN);

            if (account == null)
            {
                throw new ArgumentException("No account found with the provided IBAN.");
            }

            if (account.Amount < amount || account.Currency != currency)
            {
                throw new InvalidOperationException("The account does not have sufficient funds in the specified currency.");
            }

            account.Amount -= amount;

            var matchingAccount = await _db.Accounts.FirstOrDefaultAsync(a => a.Currency == currency && a.IBAN != IBAN);

            if (matchingAccount == null)
            {
                throw new InvalidOperationException("No account found with the specified currency.");
            }

            matchingAccount.Amount += amount;
        }

        public async Task OuterTransaction(int amount, Currency currency, string fromIBAN, string toIBAN)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var fromAccount = await _db.Accounts.FirstOrDefaultAsync(a => a.IBAN == fromIBAN);

                    if (fromAccount == null)
                    {
                        throw new ArgumentException("No account found with the provided 'from' IBAN.");
                    }

                    if (fromAccount.Amount < amount || fromAccount.Currency != currency)
                    {
                        throw new InvalidOperationException("The account does not have sufficient funds in the specified currency.");
                    }

                    fromAccount.Amount -= amount;

                    var toAccount = await _db.Accounts.FirstOrDefaultAsync(a => a.IBAN == toIBAN);

                    if (toAccount == null)
                    {
                        throw new ArgumentException("No account found with the provided 'to' IBAN.");
                    }

                    var isSameBank = fromAccount.UserId == toAccount.UserId;

                    decimal exchangeRate = isSameBank ? 1 : 1.5M;

                    decimal transferAmount = amount * exchangeRate;

                    if (toAccount.Currency != currency)
                    {
                        throw new InvalidOperationException("The 'to' account does not have the same currency as the transfer.");
                    }

                    toAccount.Amount += transferAmount;

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
