﻿using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories
{
	public class ATMRepository : IATMRepository
	{
		private readonly AppDbContext _db;
		public ATMRepository(AppDbContext db) 
		{
			_db = db;
		}

		public async Task<CardEntity> GetCardByCardNumberAsync(int cardNumber)
		{
			return await _db.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
		}
	}
}
