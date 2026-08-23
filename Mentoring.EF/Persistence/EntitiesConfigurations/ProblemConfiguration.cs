using Mentoring.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.EF.Persistence.EntitiesConfigurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        // 1. المفتاح الأساسي
        builder.HasKey(p => p.Id);

        // 2. قيود الحقول
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200); // 200 حرف كافية جداً لاسم المسألة

        builder.Property(p => p.Link)
               .IsRequired()
               .HasMaxLength(1000); // روابط المسائل (مثل LeetCode أو Codeforces) قد تكون طويلة أحياناً

        builder.Property(p => p.Notes)
               .IsRequired(false) // اختياري (لأنه Nullable في الكلاس)
               .HasMaxLength(2000); // مساحة مريحة إذا أراد المنشئ كتابة ملاحظات طويلة


        // 3. علاقة المنشئ (CreatedBy)
        builder.HasOne(p => p.ApplicationUser)
               .WithMany() // نتركها فارغة إذا لم نضف ICollection<Problem> في جدول الـ User
               .HasForeignKey(p => p.CreatedById)
               .IsRequired()
               // نفس القاعدة الهندسية هنا: Restrict لمنع حذف المستخدم الذي أنشأ مسائل
               .OnDelete(DeleteBehavior.Restrict);

        // 4. علاقات الجداول المرتبطة
        builder.HasMany(p => p.ProblemGroups)
               .WithOne(pg => pg.Problem)
               .HasForeignKey(pg => pg.ProblemId)
               // إذا تم حذف المسألة، يتم حذف ارتباطاتها في المجموعات تلقائياً
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.TraineeProblems)
               .WithOne(sp => sp.Problem)
               .HasForeignKey(sp => sp.ProblemId)
               // إذا تم حذف المسألة، يتم مسح كل سجلات حلول الطلاب المرتبطة بها
               .OnDelete(DeleteBehavior.Cascade);
    }
}
