using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories;
using BankSystem.Services;
using Moq;

namespace TransactionTests
{
	public class Tests
	{
		private Mock<ITransactionRepository> _transactionRepository;
		private Mock<IConverterService> _converterService;
		private TransactionService _transactionService;

			[SetUp]
		public void Setup()
		{

			_transactionRepository = new Mock<ITransactionRepository>();
			_converterService = new Mock<IConverterService>();
			_transactionService = new TransactionService(_transactionRepository.Object, _converterService.Object);
		}

		[Test]
		public async Task OutTransactionAsync_SuccessfulTransaction_ReturnsTransactionId()
		{
			// Arrange
			var fromAccount = new AccountEntity { IBAN = "fromIBAN", Amount = 5000, Currency = Currency.EUR };
			var toAccount = new AccountEntity { IBAN = "toIBAN", Amount = 0, Currency = Currency.USD };
			var amount = 1000;
			var currency = Currency.USD;
			var convertedAmount = 900;
			var fee = 1.5m;
			var transaction = new TransactionEntity
			{
				FromIBAN = fromAccount,
				ToIBAN = toAccount,
				Amount = convertedAmount,
				Currency = fromAccount.Currency,
				Fee = fee,
				TransactionDate = DateTime.UtcNow,
				Type = TransactionType.Outter
			};

			_transactionRepository.Setup(x => x.GetAccountByIBAN(fromAccount.IBAN)).ReturnsAsync(fromAccount);
			_transactionRepository.Setup(x => x.GetAccountByIBAN(toAccount.IBAN)).ReturnsAsync(toAccount);
			_converterService.Setup(x => x.ConvertAmountAsync(amount, currency, fromAccount.Currency)).ReturnsAsync(convertedAmount);
			_transactionRepository.Setup(x => x.CreateTransactionAsync(transaction)).ReturnsAsync(transaction.Id);

			// Act
			var result = await _transactionService.OutTransactionAsync(fromAccount.IBAN, toAccount.IBAN, amount, currency);

			// Assert
			Assert.AreEqual(transaction.Id, result);
		}



		[Test]
		public void OutTransactionAsync_WhenFromAccountDoesNotExist_ShouldThrowException()
		{
			// Arrange
			string fromIBAN = "NL01INHO0000000001";
			string toIBAN = "NL02INHO0000000002";
			decimal amount = 100;
			Currency currency = Currency.EUR;

			_transactionRepository.Setup(x => x.GetAccountByIBAN(fromIBAN)).ReturnsAsync((AccountEntity)null);

			// Act & Assert
			Assert.ThrowsAsync<Exception>(() => _transactionService.OutTransactionAsync(fromIBAN, toIBAN, amount, currency));
		}

		[Test]
		public void OutTransactionAsync_WhenToAccountDoesNotExist_ThrowsException()
		{
			// Arrange
			string fromIBAN = "NL01INHO0000000001";
			string toIBAN = "NL02INHO0000000002";
			decimal amount = 100;
			Currency currency = Currency.EUR;

			AccountEntity fromAccount = new AccountEntity { IBAN = fromIBAN, Amount = 200, Currency = Currency.EUR };

			_transactionRepository.Setup(x => x.GetAccountByIBAN(fromIBAN)).ReturnsAsync(fromAccount);
			_transactionRepository.Setup(x => x.GetAccountByIBAN(toIBAN)).ReturnsAsync((AccountEntity)null);

			// Act & Assert
			Assert.ThrowsAsync<Exception>(() => _transactionService.OutTransactionAsync(fromIBAN, toIBAN, amount, currency));
		}

		
		[Test]
		public async Task OutTransactionAsync_InsufficientFunds_ShouldThrowException()
		{
			// Arrange
			var fromAccount = new AccountEntity
			{
				IBAN = "NL91ABNA0417164300",
				Currency = Currency.EUR,
				Amount = 100
			};
			var toAccount = new AccountEntity
			{
				IBAN = "NL91ABNA0417164301",
				Currency = Currency.EUR,
				Amount = 0
			};
			var amount = 200m;
			var currency = Currency.EUR;

			_transactionRepository.Setup(r => r.GetAccountByIBAN(fromAccount.IBAN)).ReturnsAsync(fromAccount);
			_transactionRepository.Setup(r => r.GetAccountByIBAN(toAccount.IBAN)).ReturnsAsync(toAccount);

			// Act and Assert
			var ex = Assert.ThrowsAsync<Exception>(() => _transactionService.OutTransactionAsync(fromAccount.IBAN, toAccount.IBAN, amount, currency));
			Assert.That(ex.Message, Is.EqualTo("Insufficient funds"));
		}


	}
}