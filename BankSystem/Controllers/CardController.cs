using BankSystem.Models.Requests;
using BankSystem.Repositories;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("add-card")]
        public async Task<IActionResult> AddCard(AddCardRequest request)
        {
            await _cardRepository.AddCardAsync(request);

            return Ok();
        }

        [HttpPost("get-user-cards")]
        public async Task<IActionResult> GetUserCards(int userId)
        {
            var userCard = await _cardRepository.GetUserCardsAsync(userId);

            return Ok(userCard);
        }
    }
}
