using BankSystem.Db;
using BankSystem.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TransactionController : ControllerBase
//    {
//		private readonly AppDbContext _db;
//		public TransactionController(AppDbContext db) 
//		{
//			_db = db;
//		}

//		[HttpPost("accounts/{accountId}/transfer")]
//		public IActionResult TransferMoney([FromBody] TransferInnerRequest request)
//		{
//			var account = _db.Accounts.FirstOrDefault(a => a.Id == request.AccountId);
//			if (account == null)
//			{
//				return NotFound("Such an Account does not exist nigga");
//			}

//			var targetAccount = _db.Accounts.FirstOrDefault(a => a.Id == request.TargetAccountId);
//			if (targetAccount == null)
//			{
//				return NotFound("Account you want to tranfer money does not exist bro");
//			}

//			if (account.Amount < request.Amount)
//			{
//				return BadRequest("Insufficient funds.");
//			}

//			account.Amount -= request.Amount;
//			targetAccount.Amount += request.Amount;

//			_db.SaveChanges();

//			return Ok();
//		}

//		[HttpPost("accounts/{accountId}/transfer-to-external")]
//		public IActionResult TransferMoneyToExternal([FromBody] TransferOutterRequest model)
//		{
//			var account = _db.Accounts.FirstOrDefault(a => a.Id == model.AccountId);
//			if (account == null)
//			{
//				return NotFound();
//			}

		
//			var fee = ragaca * 0.01 + 0.5;

//			if (account.Amount < model.Amount + fee)
//			{
//				return BadRequest("Insufficient funds.");
//			}

//			account.Amount -= model.Amount + fee;

//			_db.SaveChanges();

//			return Ok();
//		}


//	}
//}
