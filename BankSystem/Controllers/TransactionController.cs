using BankSystem.Db;
using BankSystem.Models.Requests;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly AppDbContext _db;

        public TransactionController(
            TransactionService transactionService,
            AppDbContext db)
        {
            _transactionService = transactionService;
            _db = db;
        }

        [HttpPost("inner-transfer")]
        public async Task<IActionResult> InnerTransfer([FromBody] TransferInnerRequest innerTransferRequest)
        {
            //try
            //{
            //    await _transactionService.InnerTransactionAsync(innerTransferRequest.FromIBAN!, innerTransferRequest.ToIBAN!, innerTransferRequest.Amount);
            //    return Ok("Transfer successful");
            //}
            //catch (ArgumentException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request");
            //}

            await _transactionService.InnerTransactionAsync(innerTransferRequest.FromIBAN!, innerTransferRequest.ToIBAN!, innerTransferRequest.Amount, innerTransferRequest.Currency);
            return Ok("Transfer successful");
        }

        [HttpPost("out-transfer")]
        public async Task<IActionResult> OutTransfer([FromBody] TransferOutterRequest outTransferRequest)
        {
            //try
            //{
            //    await _transactionService.OutTransactionAsync(outTransferRequest.FromIBAN!, outTransferRequest.ToIBAN!, outTransferRequest.Amount, outTransferRequest.Currency);
            //    return Ok("Transfer successful");
            //}
            //catch (ArgumentException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request");
            //}

            await _transactionService.OutTransactionAsync(outTransferRequest.FromIBAN!, outTransferRequest.ToIBAN!, outTransferRequest.Amount, outTransferRequest.Currency);
            return Ok("Transfer successful");
        }
    }
}
