using BankSystem.Models.Requests;
using BankSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("get-account")]
        public async Task<IActionResult> GetAccountAsync(int userId)
        {
            var account = await _accountRepository.GetAccountAsync(userId);
            return Ok(account);
        }
    }
}
