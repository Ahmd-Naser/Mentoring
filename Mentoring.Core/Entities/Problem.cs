
namespace Mentoring.Core.Entities;

public class Problem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string? Notes { get; set; } 

    public ICollection<StudentProblem> StudentProblems { get; set; } = new List<StudentProblem>();
    public ICollection<ProblemGroup> ProblemGroups { get; set; } = new List<ProblemGroup>();

}
