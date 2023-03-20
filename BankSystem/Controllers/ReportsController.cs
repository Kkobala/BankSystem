using BankSystem.Db;
using BankSystem.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly AppDbContext _db;
		public ReportsController(AppDbContext db)
		{
			_db = db;
		}

		[HttpGet("user-stats-year")]
		public async Task<IActionResult> GetStatsThisYear()
		{
			var currentDate = DateTime.Now;
			var usersThisYear = await _db.Users.CountAsync(u => u.RegisteredAt.Year == currentDate.Year);
			return Ok(usersThisYear);
		}


		[HttpGet("user-stats-last-year")]
		public async Task<IActionResult> GetStatsLastYear()
		{
			var lastYearDate = DateTime.Now.AddYears(-1);
			var usersLastYear =  await _db.Users.CountAsync(u => u.RegisteredAt.Year == lastYearDate.Year);
			return Ok(usersLastYear);
		}

		[HttpGet("transaction-statistics-average-revenue")]
		public async Task<IActionResult> GetAverageRevenue()
		{
			var last30DaysDate = DateTime.Now.AddDays(-30);

			var averageRevenueGEL = await _db.Transactions
				.Where(x => x.Type == TransactionType.Inner && x.Currency == Currency.GEL)
				.AverageAsync(t => t.Amount);

			var averageRevenueUSD = await _db.Transactions
				.AverageAsync(t => t.Amount / 2.77m);

			var averageRevenueEUR = await _db.Transactions
				.AverageAsync(t => t.Amount / 2.75m);

			var result = new
			{
				AverageRevenueGEL = averageRevenueGEL,
				AverageRevenueUSD = averageRevenueUSD,
				AverageRevenueEUR = averageRevenueEUR,
				
			};

			return Ok(result);
		}

		[HttpGet("transaction-statistics-according-to-days")]
		public async Task<IActionResult> GetTransactionStatsByDay()
		{
			var last30DaysDate = DateTime.Now.AddDays(-30);

			var transactionsByDay = await _db.Transactions
				.Where(t => t.TransactionDate >= last30DaysDate)
				.GroupBy(t => t.TransactionDate.Day)
				.Select(g => new { Day = g.Key, Count = g.Count() })
				.ToListAsync();

			var result = new
			{
				transactionsByDay = transactionsByDay,
			};
			return Ok(result);
		}

	}
}

