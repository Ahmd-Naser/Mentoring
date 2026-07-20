using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Problem;

public record CreateProblemRequest(
    string Name,
    string Link,
    string? Notes
);