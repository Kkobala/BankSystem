﻿using BankSystem.Db.Entities;
using BankSystem.Models.Enums;
using BankSystem.Repositories.Interfaces;
using BankSystem.Services.Interfaces;
using BankSystem.UnitofWork;

namespace BankSystem.Services.Implementations
{

	public class TransactionService : ITransactionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConverterService _converterService;

        public TransactionService(
			IConverterService converterService,
			IUnitOfWork unitOfWork)
		{
			_converterService = converterService;
			_unitOfWork = unitOfWork;
		}

		public async Task<decimal> InnerTransactionAsync(string fromIBAN, string toIBAN, decimal amount)
		{
			var fromiban = await _unitOfWork.AccountRepository.GetAccountByIBAN(fromIBAN);
			var toiban = await _unitOfWork.AccountRepository.GetAccountByIBAN(toIBAN);

			ValidateAccountWithIBAN(fromiban, toiban);
			CheckSenderAndRecieverId(fromiban, toiban);
			CheckAmount(amount);

			decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, fromiban.Currency, toiban.Currency);

			if (fromiban.Amount < convertedAmount)
			{
				throw new Exception("Insufficient funds");
			}

			var fee = convertedAmount * 0.00m;

			var transaction = new TransactionEntity
			{
				FromAccount = fromiban,
				ToAccount = toiban,
				Amount = convertedAmount,
				Currency = fromiban.Currency,
				Fee = fee,
				TransactionDate = DateTime.UtcNow,
				Type = TransactionType.Inner
			};

			fromiban.Amount -= amount + fee;
			toiban.Amount += convertedAmount;

			await _unitOfWork.TransactionRepository.CreateTransactionAsync(transaction);

			return toiban.Amount;
		}

		public async Task<decimal> OutTransactionAsync(string fromIBAN, string toIBAN, decimal amount)
		{
			var fromiban = await _unitOfWork.AccountRepository.GetAccountByIBAN(fromIBAN);
			var toiban = await _unitOfWork.AccountRepository.GetAccountByIBAN(toIBAN);

			ValidateAccountWithIBAN(fromiban, toiban);
			CheckUsersIdNotBeSame(fromiban, toiban);
			CheckAmount(amount);
			CheckSenderBalance(amount, fromiban);

			decimal convertedAmount = await _converterService.ConvertAmountAsync(amount, fromiban.Currency, toiban.Currency);

			var fee = convertedAmount * 0.01m + 0.5m;

			var transaction = new TransactionEntity
			{
				FromAccount = fromiban,
				ToAccount = toiban,
				Amount = convertedAmount,
				Currency = fromiban.Currency,
				Fee = fee,
				TransactionDate = DateTime.UtcNow,
				Type = TransactionType.Outter
			};

			fromiban.Amount -= amount + fee;

			toiban.Amount += convertedAmount;

			await _unitOfWork.TransactionRepository.CreateTransactionAsync(transaction);

			return toiban.Amount;
		}

		private static void CheckSenderBalance(decimal amount, AccountEntity fromiban)
		{
			if (fromiban.Amount < amount)
			{
				throw new Exception("Insufficient funds");
			}
		}

		private static void CheckUsersIdNotBeSame(AccountEntity fromiban, AccountEntity toiban)
		{
			if (fromiban.UserId == toiban.UserId)
			{
				throw new Exception("Sender and receiver must not be the same user");
			}
		}

		private static void CheckAmount(decimal amount)
		{
			if (amount <= 0)
			{
				throw new Exception("Amount must be greater than 0");
			}
		}

		private static void CheckSenderAndRecieverId(AccountEntity fromiban, AccountEntity toiban)
		{
			if (fromiban.UserId != toiban.UserId)
			{
				throw new Exception("Sender and receiver must be the same user");
			}
		}

		private static void ValidateAccountWithIBAN(AccountEntity fromiban, AccountEntity toiban)
		{
			if (fromiban == null || toiban == null)
			{
				throw new Exception("One or more account(s) not found");
			}
		}
	}
}
