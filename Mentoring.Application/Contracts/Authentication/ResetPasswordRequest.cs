using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public record ResetPasswordRequest(
    string UserId,
    string Code,
    string NewPassword
);