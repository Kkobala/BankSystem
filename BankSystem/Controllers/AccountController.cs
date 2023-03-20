using BankSystem.Models.Requests;
using BankSystem.Models.Responses;
using BankSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAsync([FromQuery]CreateAccountRequest request)
        {
            var accountId = await _accountRepository.CreateAsync(request);

            return Ok(accountId);
        }
	

        [HttpGet("get-account")]
        public async Task<IActionResult> GetAccountAsync(int Id)
        {
            var account = await _accountRepository.GetAccountAsync(Id);
            return Ok(account);
        }
    }
}
