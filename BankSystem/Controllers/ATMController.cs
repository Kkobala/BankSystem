using BankSystem.Models.Requests;
using BankSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ATMController : ControllerBase
	{
		private readonly IATMService _atmService;

		public ATMController(IATMService atmService)
		{
			_atmService = atmService;
		}
		

		[HttpPost("authorize")]
		public async Task<IActionResult> AuthorizeCard(string cardNumber, int pinCode)
		{
			var (success, message) = await _atmService.AuthorizeCardAsync(cardNumber,pinCode);

			if (!success)
			{
				return BadRequest(message);
			}

			return Ok(message);
		}

		[HttpGet("get-balance/{cardNumber}")]
		public async Task<IActionResult> GetBalanceAsync(string cardNumber)
		{
			var balance = await _atmService.GetBalanceAsync(cardNumber);
			return Ok(new { Balance = balance });
		}
	}
}
