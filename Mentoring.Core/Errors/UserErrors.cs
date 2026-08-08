using Mentoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid username Or password", StatusCodes.Status401Unauthorized);

    public static readonly Error DisabledUser =
       new("User.DisabledUser", "Disabled User , please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error LockedUser =
       new("User.LockedUser", "Locked User , please contact your administrator", StatusCodes.Status401Unauthorized);


    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Operation Failed", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exist", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new("User.InvalidCode", "the code is invalid", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error NotFound =
        new("User.NotFound", "User is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidRoles =
        new("User.InvalidRoles", "Invalid Roles", StatusCodes.Status400BadRequest);
}