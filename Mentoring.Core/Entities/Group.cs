
namespace Mentoring.Core.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<StudentProblem> StudentProblems { get; set; } = new List<StudentProblem>();
    public ICollection<ProblemGroup> ProblemGroups { get; set; } = new List<ProblemGroup>();

}
