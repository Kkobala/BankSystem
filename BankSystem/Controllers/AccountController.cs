using BankSystem.Models.Requests;
using BankSystem.Repositories;
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
        public async Task<IActionResult> CreateAsync(CreateAccountRequest request)
        {

            var accountId = await _accountRepository.CreateAsync(request);
            //var response = new CreateAccountResponse();
            //response.Id = accountId;

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
