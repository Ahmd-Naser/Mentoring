using Mentoring.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.TraineeProblem;

public record TraineeProblemResponse(
    int Id,
    string ProblemName,
    string  ProblemLink,
    ProblemStatus Status,
    DateTime? LastStartedAt
);