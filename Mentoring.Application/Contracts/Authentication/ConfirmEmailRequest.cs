using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public record ConfirmEmailRequest(
    string UserId,
    string Code
);