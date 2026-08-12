using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword
);