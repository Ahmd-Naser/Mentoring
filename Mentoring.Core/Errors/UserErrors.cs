using Mentoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class UserErrors
{
    public static readonly Error NotFound =
        new("User.NotFound", "No User was found with the given ID", StatusCodes.Status404NotFound);
}