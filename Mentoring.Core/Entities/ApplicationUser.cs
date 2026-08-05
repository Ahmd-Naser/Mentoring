using Microsoft.AspNetCore.Identity;

namespace Mentoring.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public ICollection<Group> OwnedGroups { get; set; } = new List<Group>();
    public ICollection<Problem> CreatedProblems { get; set; } = new List<Problem>();
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<TraineeProblem> TraineeProblems { get; set; } = new List<TraineeProblem>();
}
