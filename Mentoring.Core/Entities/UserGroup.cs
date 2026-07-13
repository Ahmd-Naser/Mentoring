using Mentoring.Core.Enums;

namespace Mentoring.Core.Entities;

public class UserGroup
{
    public int UserId { get; set; }
    public int GroupId { get; set; }
    public GroupRoles Role { get; set; }

    public ApplicationUser User { get; set; } = default!;
    public Group Group { get; set; } = default!;
}
