using Mentoring.Core.Abstractions.Consts;
using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        builder.HasData([
            new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
                IsDefault = false,  
            },
            new ApplicationRole
            {
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
                IsDefault = true
            }
        ]);
    }
}