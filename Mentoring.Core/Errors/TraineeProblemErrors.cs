using Mentoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class TraineeProblemErrors
{
    public static readonly Error NotFound =
        new("TraineeProblemErrors.NotFound", "The specified problem was not found for this trainee in the given group.", StatusCodes.Status404NotFound);
   
    
    public static readonly Error AlreadyStarted =
        new("TraineeProblemErrors.AlreadyStarted", "The specified problem has already been started by this trainee.", StatusCodes.Status409Conflict);
}