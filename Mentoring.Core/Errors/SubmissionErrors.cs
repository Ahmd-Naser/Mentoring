
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class SubmissionErrors
{
    public static readonly Error NotFound =
        new("SubmissionErrors.NotFound", "The specified Submission was not found.", StatusCodes.Status404NotFound);
    public static readonly Error Duplicated =
        new("SubmissionErrors.Duplicated", "The specified Submission Already Exists.", StatusCodes.Status409Conflict);


}