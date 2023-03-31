using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Tests
{
    public class InnerTransactionTests
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Create a new in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BankSystem")
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Initialize the InternalTransactionsRepository with the test database context
            var transactionRepository = new TransactionRepository(_context);
            var converterService = new ConverterService(transactionRepository);
            var accountRepository = new AccountRepository(_context);

            var transactionService = new TransactionService(
                transactionRepository,
                converterService,
                accountRepository);

            // Add some test data to the BankAccounts table
            _context.Accounts.Add(new AccountEntity
            {
                IBAN = "GE29CDK60161331926819",
                Currency = Currency.GEL,
                Amount = 1000.00m
            });

            _context.Accounts.Add(new AccountEntity
            {
                IBAN = "FR1420041010050500013M02606",
                Currency = Currency.USD,
                Amount = 1000.00m
            });

            _context.Accounts.Add(new AccountEntity
            {
                IBAN = "GB29NWBK60161331926819",
                Currency = Currency.EUR,
                Amount = 1000.00m
            });

            // Add exchange rates to the in-memory database for testing
            var exchangeRates = new List<ExchangeRateEntity>
            {
                new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.GEL,
                    CurrencyTo = Currency.USD,
                    Rate = 0.361m,
                },
                new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.USD,
                    CurrencyTo = Currency.GEL,
                    Rate = 2.77m
                },
                new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.GEL,
                    CurrencyTo = Currency.EUR,
                    Rate = 0.3636m
                },
                new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.EUR,
                    CurrencyTo = Currency.GEL,
                    Rate = 2.87m
                },
                new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.USD,
                    CurrencyTo = Currency.EUR,
                    Rate = 0.98m
                },
                 new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.EUR,
                    CurrencyTo = Currency.USD,
                    Rate = 1.0071m
                },
                 new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.GEL,
                    CurrencyTo = Currency.GEL,
                    Rate = 1.0m
                },
                 new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.EUR,
                    CurrencyTo = Currency.EUR,
                    Rate = 1.0m
                },
                 new ExchangeRateEntity
                {
                    CurrencyFrom = Currency.USD,
                    CurrencyTo = Currency.USD,
                    Rate = 1.0m
                }
            };

            var allRates = _context.Rates.ToList();
            _context.Rates.RemoveRange(allRates);

            _context.Rates.AddRange(exchangeRates);
            _context.SaveChanges();
        }

        [TestCase("ABC123", "DEF456", 100, Currency.GEL, 100)]
        [TestCase("GHI789", "JKL012", 200, Currency.GEL, 200)]
        public async Task Test_InnerTransactionAsync_In_GEL(
            string fromIBAN,
            string toIBAN,
            decimal amount,
            Currency toCurrency,
            int expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 7,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 8,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.GEL
            };

            _context.Accounts.Add(fromAccount);
            _context.Accounts.Add(toAccount);
            _context.SaveChanges();

            var transactionRepository = new TransactionRepository(_context);

            var accountRepository = new AccountRepository(_context);

            var converterService = new ConverterService(transactionRepository);

            var transactionService = new TransactionService(transactionRepository, converterService, accountRepository);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }

        [TestCase("ABC123", "DEF456", 100, Currency.USD, 36.1)]
        [TestCase("GHI789", "JKL012", 50, Currency.USD, 18.05)]
        public async Task Test_InnerTransactionAsync_From_GEL_To_USD(
            string fromIBAN,
            string toIBAN,
            decimal amount,
            Currency toCurrency,
            decimal expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 15,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 16,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.USD
            };

            _context.Accounts.Add(fromAccount);
            _context.Accounts.Add(toAccount);
            _context.SaveChanges();

            var transactionRepository = new TransactionRepository(_context);

            var accountRepository = new AccountRepository(_context);

            var converterService = new ConverterService(transactionRepository);

            var transactionService = new TransactionService(transactionRepository, converterService, accountRepository);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }

        [TestCase("ABC123", "DEF456", 100, Currency.EUR, 36.36)]
        [TestCase("GHI789", "JKL012", 50, Currency.EUR, 18.18)]
        public async Task Test_InnerTransactionAsync_From_GEL_To_EUR(
            string fromIBAN,
            string toIBAN,
            decimal amount,
            Currency toCurrency,
            decimal expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 20,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 22,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.EUR
            };

            _context.Accounts.Add(fromAccount);
            _context.Accounts.Add(toAccount);
            _context.SaveChanges();

            var transactionRepository = new TransactionRepository(_context);

            var accountRepository = new AccountRepository(_context);

            var converterService = new ConverterService(transactionRepository);

            var transactionService = new TransactionService(transactionRepository, converterService, accountRepository);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }
    }
}