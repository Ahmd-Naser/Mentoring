using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        // 1. المفتاح الأساسي
        builder.HasKey(g => g.Id);

        // 2. قيود الحقول (Properties Constraints)
        builder.Property(g => g.Name)
               .IsRequired()
               .HasMaxLength(100); // تحديد طول أقصى ضروري جداً للأداء في البحث (Indexing)

        builder.Property(g => g.Description)
               .HasMaxLength(500); // حتى لا يحجز EF Core مساحة (nvarchar(max)) غير مبررة

        builder.HasOne(g => g.Owner)
            .WithMany(u => u.OwnedGroups)
            .HasForeignKey(g => g.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // منع حذف المستخدم إذا كان يمتلك مجموعات

        builder.HasMany(g => g.UserGroups)
            .WithOne(ug => ug.Group)
            .HasForeignKey(ug => ug.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // حذف جميع UserGroups عند حذف المجموعة

        builder.HasMany(g => g.TraineeProblems)
            .WithOne(sp => sp.Group)
            .HasForeignKey(sp => sp.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // حذف جميع StudentProblems عند حذف المجموعة

        builder.HasMany(g => g.ProblemGroups)
            .WithOne(pg => pg.Group)
            .HasForeignKey(pg => pg.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // حذف جميع ProblemGroups عند حذف المجموعة
    }
}
