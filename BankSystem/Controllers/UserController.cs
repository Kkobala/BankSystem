using BankSystem.Auth;
using BankSystem.Db;
using BankSystem.Db.Entities;
using BankSystem.Models;
using BankSystem.Models.AuthRequests;
using BankSystem.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _db;
        private readonly BankSystemValidations _validation;

        public UserController(
            UserManager<UserEntity> userManager,
            TokenGenerator tokenGenerator,
            AppDbContext db,
            BankSystemValidations validations)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _db = db;
            _validation = validations;
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

            var result = await _userManager.CreateAsync(entity, request.Password!);
           
            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            await _userManager.AddToRoleAsync(entity, "Operator");

            _validation.CheckPrivateNumberFormat(request.PersonalNumber);
            _validation.CheckNameOrSurname(request.Name);

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

            var isOperator = await _userManager.IsInRoleAsync(user, "operator");

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [HttpPost("operator-login")]
        public async Task<IActionResult> LoginOperator([FromQuery] LoginRequest request)
        {
            var Operator = await _userManager.FindByEmailAsync(request.Email);

            if (Operator == null)
            {
                return NotFound("Operator not found jima");
            }

            var isCoorrectPassword = await _userManager.CheckPasswordAsync(Operator, request.Password);

            if (!isCoorrectPassword) 
            {
                return BadRequest("Invalid Password or Email jimson");
            }

			var isOperator = await _userManager.IsInRoleAsync(Operator, "operator");

            if (!isOperator) 
            {
                return BadRequest("There is no such role brodie");
            }
			var roles = await _userManager.GetRolesAsync(Operator);

			return Ok(_tokenGenerator.Generate(Operator.Id.ToString(),roles));
		}
    }
}
