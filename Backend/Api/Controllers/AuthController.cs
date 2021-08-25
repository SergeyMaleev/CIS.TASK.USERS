using Api.Models;
using Api.Services;
using Auth.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IUserService _userService;
        IOptions<AuthOptions> _authOptions;

        public AuthController(IUserService userService, IOptions<AuthOptions> authOptions)
        {
            _userService = userService;
            _authOptions = authOptions;
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] LoginModels request)
        {
            var user = _userService.Authenticate(request.Email, request.Password);

            if (user != null) 
            {
                var token = _userService.GenerateJWT(user);

                return Ok(new
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    access_token = token,
                    token_live_time = _authOptions.Value.TokenLiveTime

                }); 
            }

            return Unauthorized();
        }       
    }
}
