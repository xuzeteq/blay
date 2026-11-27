using Microsoft.AspNetCore.Mvc;

namespace Modules.Users.Features.GetAllUsers
{
    [ApiController]
    [Route("api/users")]
    public class GetAllUsersController : ControllerBase
    {
        private readonly GetAllUsersHandler _handler;
        public GetAllUsersController(GetAllUsersHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken cts)
        {
            var users = await _handler.HandleAsync(cts);
            return Ok(users);
        }
    }
}
