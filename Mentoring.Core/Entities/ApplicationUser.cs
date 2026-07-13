using Microsoft.AspNetCore.Identity;

namespace Mentoring.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<StudentProblem> StudentProblems { get; set; } = new List<StudentProblem>();
}
