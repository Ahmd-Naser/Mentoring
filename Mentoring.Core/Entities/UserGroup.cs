using Mentoring.Core.Enums;

namespace Mentoring.Core.Entities;

public class UserGroup
{
    public string UserId { get; set; } = default!;
    public int GroupId { get; set; }
    public GroupRoles Role { get; set; }

    public ApplicationUser User { get; set; } = default!;
    public Group Group { get; set; } = default!;
}
