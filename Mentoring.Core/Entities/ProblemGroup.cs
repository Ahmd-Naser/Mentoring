namespace Mentoring.Core.Entities;

public class ProblemGroup
{
    public int ProblemId { get; set; }
    public int GroupId { get; set; }
    public DateTime? Deadline { get; set; } = default!;

    public Problem Problem { get; set; } = default!;
    public Group Group { get; set; } = default!;
}