using BankSystem.Models.Requests;
using BankSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize(Policy = "Operator", AuthenticationSchemes = "Bearer")]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAsync([FromQuery] CreateAccountRequest request)
        {
            var accountId = await _accountRepository.CreateAsync(request);
			return Ok(accountId);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccountAsync()
        {
			var authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(authenticatedUserId))
			{
				throw new ArgumentException("User ID not found in token");
			}

			var account = await _accountRepository.GetAccountAsync(authenticatedUserId);
            return Ok(account);
        }
    }
}
