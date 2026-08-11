using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Users;

public record UpdateProfileRequest(
    string FirstName,
    string LastName

);
