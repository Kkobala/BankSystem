using BankSystem.Models.Requests;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var card = await _cardRepository.AddCardAsync(request);

            return Ok(card);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
		[HttpGet("get-user-cards")]
        public async Task<IActionResult> GetUserCards()
        {
			var authenticatedUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (authenticatedUserId == null)
			{
				throw new ArgumentException("User ID not found in token");
			}

			var userCard = await _cardRepository.GetUserCardsAsync(authenticatedUserId);

            return Ok(userCard);
        }
    }
}
