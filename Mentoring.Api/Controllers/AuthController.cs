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
    public async Task<IActionResult> Login([FromBody]LoginRequest request , CancellationToken cancellationToken )
    { 
        var authResult = await _authService.GetTokenAsync(request.email, request.password, cancellationToken);

        return authResult.IsSuccess 
            ? Ok(authResult.Value) 
            : authResult.ToProblem();

    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return authResult.IsSuccess 
            ? Ok(authResult.Value) 
            : authResult.ToProblem();

    }

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var revokeResult = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return revokeResult.IsSuccess 
            ? Ok() 
            : revokeResult.ToProblem();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authService.RegisterAsync(request, cancellationToken);

        return authResult.IsSuccess
            ? Ok(authResult.Value)
            : authResult.ToProblem();

    }

}
