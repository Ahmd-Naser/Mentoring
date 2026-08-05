using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);