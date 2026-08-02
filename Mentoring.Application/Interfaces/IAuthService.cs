using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> GetTokenAsync(string email , string password, CancellationToken cancellationToken = default);
}
