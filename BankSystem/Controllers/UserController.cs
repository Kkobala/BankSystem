using BankSystem.Auth;
using BankSystem.Db.Entities;
using BankSystem.Models.AuthRequests;
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

        public UserController(
            UserManager<UserEntity> userManager,
            TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }


        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var entity = new UserEntity();
            entity.UserName = request.Email;
            entity.Name = request.Name;
            entity.LastName = request.LastName;
            entity.BirthDate = request.BirthDate;
            entity.PersonalNumber = request.PersonalNumber;
            entity.Email = request.Email;
            var result = await _userManager.CreateAsync(entity, request.Password!);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            return Ok();
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password!);

            if (!isCorrectPassword)
            {
                return BadRequest("Invalid email or password");
            }

            return Ok(_tokenGenerator.Generate(user.Id.ToString()));
        }
    }
}
