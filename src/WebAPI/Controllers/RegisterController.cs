using Application.Interfaces.RegisterService;
using Application.RoleRequeste;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegisterController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Generic registration endpoint.
        /// Each role registers itself (unauthenticated).
        /// Admin later approves/rejects.
        /// </summary>
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
     
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateRoleSession  request)
        {
            if (request?.UserRequest == null)
                return BadRequest("Invalid registration request: missing user info.");
           

            var result = await _registrationService.RegisterAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(new
            {
                message = "Registration submitted successfully. Awaiting admin approval.",
                id = result.Value
            });
        }
    }

}
