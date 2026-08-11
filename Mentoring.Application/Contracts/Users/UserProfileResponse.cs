using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Users;

public record UserProfileResponse(
    string Email,
    string UserName,
    string FirstName,
    string LastName
);