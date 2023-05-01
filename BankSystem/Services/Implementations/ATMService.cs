using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories.Interfaces;
using BankSystem.Services.Interfaces;
using BankSystem.UnitofWork;

namespace BankSystem.Services.Implementations
{

	public class ATMService : IATMService
	{
		private readonly IConverterService _converterService;
		private readonly IUnitOfWork _unitOfWork;

		public ATMService(
			IConverterService converterService,
			IUnitOfWork unitOfWork)
		{
			_converterService = converterService;
			_unitOfWork = unitOfWork;
		}

		public async Task<decimal> Withdraw(string cardNumber, int pin, decimal amount, Currency toCurrency)
		{
			var account = await _unitOfWork.AccountRepository.GetAccountByCardNumber(cardNumber);

			CheckAccountExistence(account);
			CheckAccountBalance(amount, account);
			CheckAmountToBeMoreThanZero(amount);

			var pinCode = await _unitOfWork.CardRepository.GetCardByPIN(pin);

			CheckPIN(pinCode);

			var card = await _unitOfWork.ATMRepository.GetCardByCardNumberAsync(cardNumber);

			CheckCardExistence(card);

			var fee = CalculateFee(amount, account!.Currency);

			var transaction = new TransactionEntity
			{
				AccountId = account!.Id,
				CardId = card.Id,
				CardNumber = cardNumber,
				Amount = amount,
				Currency = toCurrency,
				Fee = 0,
				TransactionDate = DateTime.UtcNow,
				Type = TransactionType.ATM
			};

			transaction.Fee = fee;
			transaction.Amount -= fee;

			var convertedAmount = await _converterService.ConvertAmountAsync(amount, account.Currency, toCurrency);

			await LimitFor24Hours(cardNumber, convertedAmount);

			transaction.Amount = convertedAmount;

			account.Amount -= convertedAmount;

			await _unitOfWork.TransactionRepository.CreateWithdrawAsync(transaction);

			return account.Amount;
		}

		private static void CheckAmountToBeMoreThanZero(decimal amount)
		{
			if (amount <= 0)
			{
				throw new ArgumentException("Please Enter Valid Amount");
			}
		}

		private static void CheckCardExistence(CardEntity card)
		{
			if (card == null)
			{
				throw new Exception("Card not found.");
			}
		}

		private static void CheckPIN(CardEntity? pinCode)
		{
			if (pinCode == null)
			{
				throw new Exception("Card with this pincode does not exist.");
			}
		}

		private static void CheckAccountBalance(decimal amount, AccountEntity? account)
		{
			if (account!.Amount < amount)
			{
				throw new Exception("Insufficient balance.");
			}
		}

		private static void CheckAccountExistence(AccountEntity? account)
		{
			if (account == null)
			{
				throw new Exception("Account not found.");
			}
		}

		private async Task LimitFor24Hours(string cardNumber, decimal amount)
		{
			var transactions = await _unitOfWork.TransactionRepository.GetTransactionsByCardNumber(cardNumber);

			var last24HoursTransactions = transactions
				.Where(t => t!.TransactionDate >= DateTime.UtcNow.AddDays(-1));

			var totalWithdrawalsLast24Hours = last24HoursTransactions
				.Where(t => t!.Type == TransactionType.ATM)
				.Sum(t => t!.Amount);

			if (totalWithdrawalsLast24Hours + amount > 10000)
			{
				throw new Exception("Withdrawal limit exceeded.");
			}
		}

		private decimal CalculateFee(decimal amount, Currency currency)
		{
			decimal fee = 0;

			if (currency == Currency.GEL)
			{
				fee = amount * 0.00m;
			}
			else if (currency == Currency.EUR)
			{
				fee = amount * 0.01m + 0.5m;
			}
			else if (currency == Currency.USD)
			{
				fee = amount * 0.01m + 0.5m;
			}

			return fee;
		}

		public async Task<(bool, string)> AuthorizeCardAsync(string cardNumber, int pin)
		{
			var card = await _unitOfWork.ATMRepository.GetCardByCardNumberAsync(cardNumber);

			if (card == null)
			{
				return (false, "Invalid card number.");
			}

			if (card.CardExpirationDate < DateTime.UtcNow)
			{
				return (false, "Card has expired.");
			}

			if (card.PIN != pin)
			{
				return (false, "Incorrect PIN.");
			}

			return (true, "Card authorized successfully.");
		}

		public async Task<decimal> GetBalanceAsync(string cardNumber, int pin)
		{
			var account = await _unitOfWork.AccountRepository.GetAccountByCardNumber(cardNumber);
			var card = await _unitOfWork.CardRepository.GetCardByPIN(pin);

			if (account == null)
			{
				throw new Exception($"Account not found for card with number {cardNumber}");
			}

			if (card == null)
			{
				throw new Exception($"Account not found for card with pin {pin}");
			}

			return account.Amount;
		}
	}
}