using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using NLog;
using Services.Abstract;

namespace NewsProject.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = _loginService.AuthenticateOrNull(login.UserName, login.Password);

                if (user != null)
                {
                    var token = _loginService.GenerateToken(user);
                    return Ok(token);
                }

                return NotFound("User not found");
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured watch in Log");
            }
            
        }
    }
}
