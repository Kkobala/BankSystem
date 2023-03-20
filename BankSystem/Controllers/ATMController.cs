using BankSystem.Models.Requests;
using BankSystem.Repositories;
using BankSystem.Services;
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

		[HttpPost("withdraw")]
		public async Task<IActionResult> Withdraw([FromQuery] WithdrawRequest request)
		{
			var transaction = await _atmService.Withdraw(request.AccountId, request.CardId, request.Amount, request.FromCurrency, request.ToCurrency);

			return Ok(transaction);
        }

        [HttpPost("change-pin")]
        public async Task<IActionResult> ChangePIN([FromQuery] ChangePINRequest request)
        {
            var changepin = await _cardRepository.ChangePINAsync(request);

            return Ok(changepin);
        }
    }
}
