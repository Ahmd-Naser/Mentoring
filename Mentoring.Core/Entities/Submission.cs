using Mentoring.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Entities;

public class Submission
{
    public int Id { get; set; }
    public int StudentProblemId { get; set; }

    public string CodeLink { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public SubmissionVerdict Verdict { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow ;

    public StudentProblem StudentProblem { get; set; } = default!;
}
