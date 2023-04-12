using BankSystem.Models.Requests;
using BankSystem.Repositories.Interfaces;
using BankSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ATMController : ControllerBase
	{
		private readonly IATMService _atmService;
		private readonly ICardRepository _cardRepository;

		public ATMController(IATMService atmService,
			ICardRepository cardRepository)
		{
			_atmService = atmService;
			_cardRepository = cardRepository;
		}

		[HttpPost("authorize-card")]
		public async Task<IActionResult> AuthorizeCard([FromBody]string cardNumber, int pinCode)
		{
			var (success, message) = await _atmService.AuthorizeCardAsync(cardNumber, pinCode);

			if (!success)
			{
				return BadRequest(message);
			}
			return Ok(message);
		}

		[HttpGet("get-balance")]
		public async Task<IActionResult> GetBalanceAsync(string cardNumber, int pin)
		{
			var balance = await _atmService.GetBalanceAsync(cardNumber, pin);

			return Ok(new { Balance = balance });
		}

		[HttpPut("withdraw")]
		public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
		{
			var transaction = await _atmService.Withdraw(request.CardNumber, request.PIN, request.Amount, request.ToCurrency);

			return Ok(transaction);
		}

		[HttpPut("change-pin")]
		public async Task<IActionResult> ChangePIN([FromBody] ChangePINRequest request)
		{
			var changepin = await _cardRepository.ChangePINAsync(request);

			return Ok(changepin);
		}
	}
}
