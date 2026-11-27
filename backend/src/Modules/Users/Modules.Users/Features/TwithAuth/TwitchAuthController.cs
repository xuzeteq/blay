using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Modules.Users.Features.TwithAuth
{
    [ApiController]
    [Route("api/auth/twitch")]
    public class TwitchAuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/api/auth/twitch/me" },
                TwitchAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Username = User.FindFirstValue(ClaimTypes.Name),
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
