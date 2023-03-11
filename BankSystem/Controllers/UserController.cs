using BankSystem.Auth;
using BankSystem.Db;
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
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly AppDbContext _db;

        public UserController(
            UserManager<UserEntity> userManager,
            TokenGenerator tokenGenerator,
            RoleManager<RoleEntity> roleManager,
            AppDbContext db)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
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

            await _userManager.AddToRoleAsync(entity, "operator");

            await _db.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
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
    }
}
