using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class ProblemGroupConfiguration : IEntityTypeConfiguration<ProblemGroup>
{
    public void Configure(EntityTypeBuilder<ProblemGroup> builder)
    {
        builder.HasKey(pg => new { pg.ProblemId, pg.GroupId });

        builder.Property(pg => pg.Deadline)
               .IsRequired(false);

        builder.HasOne(pg => pg.Problem)
            .WithMany(p => p.ProblemGroups)
            .HasForeignKey(pg => pg.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pg => pg.Group)
            .WithMany(g => g.ProblemGroups)
            .HasForeignKey(pg => pg.GroupId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
