using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public record GroupProblemResponse(
    int ProblemId,
    string Name,
    string Link,
    Difficulty Difficulty,
    DateTime? Deadline
);