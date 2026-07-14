
namespace Mentoring.Core.Entities;

public class Problem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public string CreatedById { get; set; } = default!;
    public ApplicationUser CreatedBy { get; set; } = default!;

    public ICollection<TraineeProblem> TraineeProblems { get; set; } = new List<TraineeProblem>();
    public ICollection<ProblemGroup> ProblemGroups { get; set; } = new List<ProblemGroup>();

}
