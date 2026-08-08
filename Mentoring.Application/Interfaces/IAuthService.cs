using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(string email , string password, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token , string refreshToken , CancellationToken cancellationToken = default);
    Task<Result> RevokeRefreshTokenAsync(string token , string refreshToken , CancellationToken cancellationToken = default);
}
