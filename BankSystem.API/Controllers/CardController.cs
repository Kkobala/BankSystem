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

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
		[HttpGet("get-user-cards")]
        public async Task<IActionResult> GetUserCards(int accountId)
        {
            var userCard = await _cardRepository.GetUserCardsAsync(accountId);

            return Ok(userCard);
        }
    }
}
