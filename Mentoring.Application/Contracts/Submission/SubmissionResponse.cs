using Mentoring.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Submission;

public record SubmissionResponse(
    int Id,
    string CodeLink,
    string? Notes,
    SubmissionVerdict Verdict,
    DateTime CreatedAt
);