using Mentoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class ProblemErrors
{
    public static readonly Error NotFound =
        new("Problem.NotFound", "No Problem was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Error Forbidden =
       new("Problem.Forbidden", "You Cant modify this Problem data", StatusCodes.Status403Forbidden);

}
