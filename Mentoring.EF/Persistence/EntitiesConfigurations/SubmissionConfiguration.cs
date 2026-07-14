using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.HasKey(s => s.Id);  

        builder.Property(s => s.CodeLink)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s=> s.Notes)
            .IsRequired(false)
            .HasMaxLength(1000);


        builder.Property(s => s.SubmittedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(s => s.TraineeProblem)
            .WithMany(tp => tp.Submissions)
            .HasForeignKey(s => s.TraineeProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
