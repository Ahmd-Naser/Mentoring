using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class TraineeProblemConfiguration : IEntityTypeConfiguration<TraineeProblem>
{
    public void Configure(EntityTypeBuilder<TraineeProblem> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder
            .HasIndex(sp => new { sp.UserId, sp.ProblemId, sp.GroupId })
            .IsUnique();

        builder
            .Property(sp => sp.TimeSpentInSeconds)
               .HasDefaultValue(0);

        builder
            .HasOne(sp => sp.User)
            .WithMany(u => u.TraineeProblems)
            .HasForeignKey(sp => sp.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(sp => sp.Problem)
            .WithMany(p => p.TraineeProblems)
            .HasForeignKey(sp => sp.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(sp => sp.Group)
            .WithMany(g => g.TraineeProblems)
            .HasForeignKey(sp => sp.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(sp=> sp.Submissions)
            .WithOne(s => s.TraineeProblem)
            .HasForeignKey(s => s.TraineeProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
