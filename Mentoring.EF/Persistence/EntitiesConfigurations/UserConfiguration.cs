using Mentoring.Core.Abstractions.Consts;
using Mentoring.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        //var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "Mentoring",
            LastName = "Admin",
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEKM/hAKble5pM4y/Lpy1nvDRykBSI11RzTrE6CVO/+SvLNMmEQZLkbRNb5bm6CS4Tg=="
        });

        //Console.WriteLine(hasher.HashPassword(null!, DefaultUsers.AdminPassword));
    }
}
