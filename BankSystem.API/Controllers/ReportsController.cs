using BankSystem.Db;
using BankSystem.Models.Enums;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITransactionRepository _transactionRepository;

        public ReportsController(AppDbContext db,
            ITransactionRepository transactionRepository)
        {
            _db = db;
            _transactionRepository = transactionRepository;
        }

        [HttpGet("user-stats-current-year")]
        public async Task<IActionResult> GetStatsThisYear()
        {
            var currentDate = DateTime.Now;
            var usersThisYear = await _db.Users.CountAsync(u => u.RegisteredAt.Year == currentDate.Year);

            var result = new
            {
                UsersCurrentYear = usersThisYear,
            };

            return Ok(result);
        }

        [HttpGet("user-stats-last-year")]
        public async Task<IActionResult> GetStatsLastYear()
        {
            var lastYearDate = DateTime.Now.AddYears(-1);
            var usersLastYear = await _db.Users.CountAsync(u => u.RegisteredAt.Year == lastYearDate.Year);

            var result = new
            {
                UsersLastYear = usersLastYear,
            };

            return Ok(result);
        }

        [HttpGet("last-registered-users-in-30-Days")]
        public async Task<IActionResult> LastRegisteredUsersIn30Days()
        {
            var users = await _db.Users
                       .Where(x => x.RegisteredAt >= DateTime.Now.AddDays(-30))
                       .CountAsync();

            var result = new
            {
                UsersInLast30Days = users
            };

            return Ok(result);
        }

        [HttpGet("count-transactions-in-last-onemonth-or-sixmonth-or-oneyear")]
        public async Task<IActionResult> CountLastTransactions()
        {
            var innertransactions = await _db.Transactions
                       .Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-1) ||
                       x.TransactionDate >= DateTime.Now.AddMonths(-6) || x.TransactionDate >= DateTime.Now.AddYears(-1))
                       .Where(x => x.Type == TransactionType.Inner)
                       .CountAsync();

            var outertransactions = await _db.Transactions
                       .Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-1) ||
                       x.TransactionDate >= DateTime.Now.AddMonths(-6) || x.TransactionDate >= DateTime.Now.AddYears(-1))
                       .Where(x => x.Type == TransactionType.Outter)
                       .CountAsync();

            var resutl = new
            {
                InnerTransaction = innertransactions,
                OuterTransaction = outertransactions
            };

            return Ok(resutl);
        }

        [HttpGet("count-revenue-from-transactions-in-last-onemonth-or-sixmonth-or-oneyear")]
        public async Task<IActionResult> GetRevenueAsync()
        {
            var totalrevenueinGEL = await _transactionRepository.GetAllTransactionsInGELAsync();

            var totalrevenueinUSD = await _transactionRepository.GetAllTransactionsInUSDAsync();

            var totalrevenueinEUR = await _transactionRepository.GetAllTransactionsInEURAsync();

            var result = new
            {
                TotalRevenueInGEL = totalrevenueinGEL,
                TotalRevenueInUSD = totalrevenueinUSD,
                TotalRevenueInEur = totalrevenueinEUR,
            };

            return Ok(result);
        }

        [HttpGet("transaction-statistics-average-revenue")]
        public async Task<IActionResult> GetAverageRevenue()
        {
            var last30DaysDate = DateTime.Now.AddDays(-30);

            var averageRevenueGEL = await _db.Transactions
                .Where(x => x.Currency == Currency.GEL)
                .Select(x => x.Amount)
                .DefaultIfEmpty()
                .AverageAsync();

            var averageRevenueUSD = await _db.Transactions
                .Where(x => x.Currency == Currency.USD)
                .Select(x => x.Amount)
                .DefaultIfEmpty()
                .AverageAsync();

            var averageRevenueEUR = await _db.Transactions
                .Where(x => x.Currency == Currency.EUR)
                .Select(x => x.Amount)
                .DefaultIfEmpty()
                .AverageAsync();

            var result = new
            {
                AverageRevenueGEL = averageRevenueGEL,
                AverageRevenueUSD = averageRevenueUSD,
                AverageRevenueEUR = averageRevenueEUR
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
                TransactionsByDay = transactionsByDay,
            };

            return Ok(result);
        }

        [HttpGet("Total-amount-of-money-withdraw-from-the-ATM")]
        public async Task<IActionResult> GetTotalamountofmoneywithdrawnfromtheATM()
        {
            var transaction = await _db.Transactions
                .Where(x => x.Type == TransactionType.ATM)
                .ToListAsync();

            var sum = transaction.Sum(x => x.Amount);

            var result = new
            {
                TotalAmount = sum,
            };

            return Ok(result);
        }
    }
}