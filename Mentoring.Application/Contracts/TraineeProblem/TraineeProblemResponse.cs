using Mentoring.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.TraineeProblem;

public record TraineeProblemResponse(
    int Id,
    ProblemStatus Status,
    DateTime? LastStartedAt
);