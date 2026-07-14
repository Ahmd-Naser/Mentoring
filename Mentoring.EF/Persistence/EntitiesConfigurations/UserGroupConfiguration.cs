using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(ug => new {ug.UserId, ug.GroupId });

        builder.HasOne(ug => ug.User)
               .WithMany(u => u.UserGroups)
               .HasForeignKey(ug => ug.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ug => ug.Group)
               .WithMany(g => g.UserGroups)
               .HasForeignKey(ug => ug.GroupId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
