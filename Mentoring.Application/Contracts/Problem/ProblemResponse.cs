using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Problem;

public record ProblemResponse(
    int Id,
    string Title,
    string Link,
    string ?Notes
);
