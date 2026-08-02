using Mentoring.Application.Contracts.Authentication;
using Mentoring.Application.Interfaces;
using Mentoring.Core.Abstractions.Consts;
using Mentoring.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mentoring.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequest request , CancellationToken cancellationToken )
    { 
        var authResult = await _authService.GetTokenAsync(request.email, request.password, cancellationToken);

        return authResult is null ? BadRequest("Username or password is invalid" ) : Ok(authResult);

    }
}
