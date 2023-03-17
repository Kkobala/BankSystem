using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
<<<<<<< HEAD
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly ILogger<ATMController> _logger;
        private readonly IATMService _atmService;

        public ATMController(ILogger<ATMController> logger, IATMService atmService)
        {
            _logger = logger;
            _atmService = atmService;
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Authorize(string cardNumber, int pincode)
        {
            try
            {
                var authorizedCard = await _atmService.AuthorizeCardAsync(cardNumber, pincode);
                return Ok(authorizedCard);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning($"Failed to authorize card. Reason: {ex.Message}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while authorizing card. Reason: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{cardNumber}/balance")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            try
            {
                var balance = await _atmService.GetBalanceAsync(cardNumber);
                return Ok(balance);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning($"Failed to retrieve balance. Reason: {ex.Message}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving balance. Reason: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
=======
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
>>>>>>> 78bf37a7022baf072a99af5192c7b4dfb5867feb
}
