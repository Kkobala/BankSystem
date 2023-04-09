using BankSystem.Auth;
using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models.AuthRequests;
using BankSystem.Models.Requests;
using BankSystem.Repositories;
using BankSystem.Services;
using BankSystem.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternetBankController : ControllerBase
    {

        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITransactionService _transactionService;
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;
        private readonly AppDbContext _db;
        private readonly BankSystemValidations _validation;

        public InternetBankController(
            ICardRepository cardRepository,
            ITransactionService transactionService,
            IAccountRepository accountRepository,
            AppDbContext db,
            TokenGenerator tokenGenerator,
            UserManager<UserEntity> userManager,
            BankSystemValidations validation)
        {
            _transactionService = transactionService;
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
            _db = db;
            _validation = validation;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        [HttpPost("operator-login")]
        public async Task<IActionResult> LoginOperator([FromQuery] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound("Operator not found");
            }

            var isCoorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCoorrectPassword)
            {
                return BadRequest("Invalid Password or Email");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "Operator", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromQuery] RegisterUserRequest request)
        {
            var entity = new UserEntity
            {
                UserName = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                PersonalNumber = request.PersonalNumber,
                Email = request.Email
            };

            _validation.ValidateEmailAddress(request.Email);
            _validation.CheckPrivateNumberFormat(request.PersonalNumber);
            _validation.CheckNameOrSurname(request.Name);

            var result = await _userManager.CreateAsync(entity, request.Password!);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            await _userManager.AddToRoleAsync(entity, "user");

            await _db.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromQuery] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password!);

            if (!isCorrectPassword)
            {
                return BadRequest("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "Operator", AuthenticationSchemes = "Bearer")]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccountAsync([FromQuery] CreateAccountRequest request)
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

        [Authorize(Policy = "Operator", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-card")]
        public async Task<IActionResult> AddCard([FromQuery] AddCardRequest request)
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

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("inner-transfer")]
        public async Task<IActionResult> InnerTransfer([FromQuery] TransferInnerRequest innerTransferRequest)
        {
            var transaction = await _transactionService.InnerTransactionAsync(innerTransferRequest.FromIBAN!, innerTransferRequest.ToIBAN!, innerTransferRequest.Amount);

            return Ok(transaction);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("out-transfer")]
        public async Task<IActionResult> OutTransfer([FromQuery] TransferOutterRequest outTransferRequest)
        {
            var transaction = await _transactionService.OutTransactionAsync(outTransferRequest.FromIBAN!, outTransferRequest.ToIBAN!, outTransferRequest.Amount);

            return Ok(transaction);
        }
    }
}
