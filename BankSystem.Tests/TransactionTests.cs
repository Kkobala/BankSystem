using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;
using BankSystem.Services;
using Moq;

namespace BankSystem.Tests
{
    public class TransactionTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private Mock<IConverterService> _converterServiceMock;

        [SetUp]
        public void Setup()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _converterServiceMock = new Mock<IConverterService>();
        }

        [TestCase("ABC123", "DEF456", 100, Currency.GEL, 100)]
        [TestCase("GHI789", "JKL012", 200, Currency.GEL, 200)]
        public async Task Test_InnerTransactionAsync_In_GEL(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency, int expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 1,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 2,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.GEL
            };

            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(fromIBAN))
                                      .ReturnsAsync(fromAccount);
            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(toIBAN))
                                      .ReturnsAsync(toAccount);

            _converterServiceMock.Setup(s => s.ConvertAmountAsync(amount, fromAccount.Currency, toCurrency))
                     .ReturnsAsync(amount);

            var transactionService = new TransactionService(_transactionRepositoryMock.Object, _converterServiceMock.Object);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }

        [TestCase("ABC123", "DEF456", 100, Currency.USD, 36.1)]
        [TestCase("GHI789", "JKL012", 50, Currency.USD, 18.05)]
        public async Task Test_InnerTransactionAsync_In_USD(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency, decimal expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 1,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 2,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.USD
            };

            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(fromIBAN))
                                      .ReturnsAsync(fromAccount);
            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(toIBAN))
                                      .ReturnsAsync(toAccount);

            decimal convertedAmount = amount * 0.361m;
            _converterServiceMock.Setup(s => s.ConvertAmountAsync(amount, fromAccount.Currency, toCurrency))
                     .ReturnsAsync(convertedAmount);

            var transactionService = new TransactionService(_transactionRepositoryMock.Object, _converterServiceMock.Object);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }

        [TestCase("ABC123", "DEF456", 100, Currency.EUR, 36.36)]
        [TestCase("GHI789", "JKL012", 50, Currency.EUR, 18.18)]
        public async Task Test_InnerTransactionAsync_In_EUR(string fromIBAN, string toIBAN, decimal amount, Currency toCurrency, decimal expectedTransaction)
        {
            var fromAccount = new AccountEntity
            {
                Id = 1,
                IBAN = fromIBAN,
                Amount = 200.0m,
                Currency = Currency.GEL
            };

            var toAccount = new AccountEntity
            {
                Id = 2,
                IBAN = toIBAN,
                Amount = 0.0m,
                Currency = Currency.EUR
            };

            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(fromIBAN))
                                      .ReturnsAsync(fromAccount);
            _transactionRepositoryMock.Setup(r => r.GetAccountByIBAN(toIBAN))
                                      .ReturnsAsync(toAccount);

            decimal convertedAmount = amount * 0.3636m;
            _converterServiceMock.Setup(s => s.ConvertAmountAsync(amount, fromAccount.Currency, toCurrency))
                     .ReturnsAsync(convertedAmount);

            var transactionService = new TransactionService(_transactionRepositoryMock.Object, _converterServiceMock.Object);

            var actualTransaction = await transactionService.InnerTransactionAsync(fromIBAN, toIBAN, amount, toCurrency);

            Assert.That(actualTransaction, Is.EqualTo(expectedTransaction));
        }
    }
}