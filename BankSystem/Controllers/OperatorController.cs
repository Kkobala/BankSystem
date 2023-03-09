using BankSystem.Auth;
using BankSystem.Db.Entities;
using BankSystem.Models.AuthRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly RoleManager<RoleEntity> _roleManager;

        public OperatorController(
            TokenGenerator tokenGenerator,
            RoleManager<RoleEntity> roleManager)
        {
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
        }

        [HttpPost("register-operator")]
        public async Task<IActionResult> RegisterOperator([FromBody] RegisterOperatorRequest request)
        {
            var entity = new RoleEntity();
            entity.Name = request.Name;
            var result = await _roleManager.CreateAsync(entity);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            return Ok(result);
        }

        [HttpPost("login-operator")]
        public async Task<IActionResult> LoginOperator([FromBody] LoginOperatorRequest request)
        {
            var loginoperator = await _roleManager.FindByNameAsync(request.Name!);

            if (loginoperator == null)
            {
                return NotFound("Operator not found");
            }

            return Ok(_tokenGenerator.Generate(loginoperator.Id.ToString()));
        }
    }
}
