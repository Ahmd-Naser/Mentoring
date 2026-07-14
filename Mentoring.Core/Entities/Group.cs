
namespace Mentoring.Core.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string OwnerId { get; set; } = default!;
    public ApplicationUser Owner { get; set; } = default!;

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<TraineeProblem> TraineeProblems { get; set; } = new List<TraineeProblem>();
    public ICollection<ProblemGroup> ProblemGroups { get; set; } = new List<ProblemGroup>();

}
