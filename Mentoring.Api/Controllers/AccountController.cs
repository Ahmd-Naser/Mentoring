

namespace Mentoring.Api.Controllers;

[Route("me")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("")]
    public async Task<IActionResult> Info()
    {
        var userId = User.GetUserId();

        
        var result = await _userService.GetProfileAsync(userId!);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateInfo([FromBody] UpdateProfileRequest request)
    {
        var userId = User.GetUserId();

        await _userService.UpdateProfileAsync(userId!, request);

        return NoContent();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.GetUserId();

        var result = await _userService.ChangePasswordAsync(userId!, request);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }
}
