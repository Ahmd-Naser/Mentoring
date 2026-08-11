using Mentoring.Api.Extensions;
using Mentoring.Application.Contracts.Users;
using Mentoring.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
}
