using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public record AssignProblemRequest(
    DateTime? Deadline
);