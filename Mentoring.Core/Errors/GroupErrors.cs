using Mentoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Errors;

public static class GroupErrors
{
    public static readonly Error NotFound =
        new("Group.NotFound", "No Group was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Error NotFoundTraineeInGroup =
        new("Group.NotFoundTraineeInGroup", "No Trainee was found with the given Id In the specific group", StatusCodes.Status404NotFound);

    public static readonly Error Forbidden =
       new("Group.Forbidden", "You Cant modify this Group data", StatusCodes.Status403Forbidden);

    public static readonly Error DuplicatedTrainee =
       new("Group.DuplicatedTrainee", "this Trainee already exist in this group", StatusCodes.Status409Conflict);

    public static readonly Error DuplicatedProblem =
       new("Group.DuplicatedProblem", "this Problem already exist in this group", StatusCodes.Status409Conflict);

    public static readonly Error NotFoundInGroup =
        new("Group.NotFoundInGroup", "No Problem was found with the given ID in the specific group", StatusCodes.Status404NotFound);
}
