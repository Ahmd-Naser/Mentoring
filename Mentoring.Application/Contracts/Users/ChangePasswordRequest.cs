using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Users;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);