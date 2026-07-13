using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Enums;

public enum SubmissionVerdict
{
    Accepted = 1,
    WrongAnswer = 2,
    TimeLimitExceeded = 3,
    CompilationError = 4,
    RuntimeError = 5,
}
