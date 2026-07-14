using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Abstractions.Consts;

public static class DefaultRoles
{
    public const string Admin = nameof(Admin);
    public const string AdminRoleId = "fe2e9330-fd4c-4114-a7a9-e23416259ed3";
    public const string AdminRoleConcurrencyStamp = "6c71566d-8646-4c0e-a86c-4802d19c91d7";

    public const string Member = nameof(Member);
    public const string MemberRoleId = "b821055c-3638-4339-b466-0338fe6bce2c";
    public const string MemberRoleConcurrencyStamp = "de97960b-98ce-473e-86c4-c49afda90253";

}
