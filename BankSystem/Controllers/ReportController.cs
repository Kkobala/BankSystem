using BankSystem.Db;
using BankSystem.Models;
using BankSystem.Models.Enums;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ConverterService _converterService;

        public ReportController(AppDbContext db,
            ITransactionRepository transactionRepository,
            ConverterService converterService)
        {
            _db = db;
            _transactionRepository = transactionRepository;
            _converterService = converterService;

        }

        [HttpGet("last-registered-users-in-30-Days")]
        public async Task<IActionResult> LastRegisteredUsersIn30Days()
        {
            var users = await _db.Users
                       .Where(x => x.RegisteredAt >= DateTime.Now.AddDays(-30))
                       .CountAsync();

            return Ok(users);
        }

        [HttpGet("count-transactions-in-last-onemonth-or-sixmonth-or-oneyear")]
        public async Task<IActionResult> CountLastTransactions()
        {
            var transactions = await _db.Transactions
                       .Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-1) ||
                       x.TransactionDate >= DateTime.Now.AddMonths(-6) || x.TransactionDate >= DateTime.Now.AddYears(-1))
                       .CountAsync();

            return Ok(transactions);
        }

        [HttpGet("count-revenue-from-transactions-in-last-onemonth-or-sixmonth-or-oneyear")]
        public async Task<IActionResult> GetRevenueAsync(Currency currency)
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            decimal totalRevenue = transactions.Sum(x => x.Amount);

            decimal revenue = _converterService.ConvertAmount(totalRevenue, transactions.First().Currency, currency);

            return Ok(revenue);
        }

        [HttpGet("Total-amount-of-money-withdraw-from-the-ATM")]
        public async Task<IActionResult> GetTotalamountofmoneywithdrawnfromtheATM()
        {
            var transaction = await _db.Transactions
                .Where(x => x.Type == TransactionType.ATM)
                .ToListAsync();

            var sum = transaction.Sum(x => x.Amount);

            return Ok(sum);
        }
    }
}
