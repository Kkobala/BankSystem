using BankSystem.Db;
using BankSystem.Models.Requests;
using BankSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly AppDbContext _db;

        public TransactionController(
            ITransactionService transactionService,
            AppDbContext db)
        {
            _transactionService = transactionService;
            _db = db;
        }

		[Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
		[HttpPost("inner-transfer")]
        public async Task<IActionResult> InnerTransfer([FromQuery] TransferInnerRequest innerTransferRequest)
        {
            await _transactionService.InnerTransactionAsync(innerTransferRequest.FromIBAN!, innerTransferRequest.ToIBAN!, innerTransferRequest.Amount, innerTransferRequest.Currency);

            return Ok("Transfer successful");
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
		[HttpPost("out-transfer")]
        public async Task<IActionResult> OutTransfer([FromQuery] TransferOutterRequest outTransferRequest)
        {
            await _transactionService.OutTransactionAsync(outTransferRequest.FromIBAN!, outTransferRequest.ToIBAN!, outTransferRequest.Amount, outTransferRequest.Currency);

            return Ok("Transfer successful");
        }
    }
}
