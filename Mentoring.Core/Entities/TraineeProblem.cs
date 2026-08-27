using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Mentoring.Core.Enums;

namespace Mentoring.Core.Entities;

public class TraineeProblem
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public int ProblemId { get; set; }
    public int GroupId { get; set; }

    public ProblemStatus Status { get; set; } = ProblemStatus.Unattempted;
    public int TimeSpentInSeconds { get; set; } = 0;
    public DateTime? LastStartedAt { get; set; } = default!;

    public ApplicationUser User { get; set; } = default!;
    public Problem Problem { get; set; } = default!;
    public Group Group { get; set; } = default!;
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();


}
