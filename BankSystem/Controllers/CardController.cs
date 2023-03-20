using BankSystem.Models.Requests;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardController(
            ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;

        }

        [Authorize(Policy = "Operator", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-card")]
        public async Task<IActionResult> AddCard([FromQuery]AddCardRequest request)
        {
            await _cardRepository.AddCardAsync(request);

            return Ok();
        }

        [HttpGet("get-user-cards")]
        public async Task<IActionResult> GetUserCards(int accountId)
        {
            var userCard = await _cardRepository.GetUserCardsAsync(accountId);

            return Ok(userCard);
        }

        [HttpPost("change-pin")]
        public async Task<IActionResult> ChangePIN(ChangePINRequest request)
        {
            var changepin = await _cardRepository.ChangePINAsync(request);

            return Ok(changepin);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromQuery]WithdrawRequest request)
        {
            var transaction = await _atmService.Withdraw(request.AccountId, request.Amount, request.FromCurrency, request.ToCurrency);

            return Ok(transaction);
        }
    }
}
