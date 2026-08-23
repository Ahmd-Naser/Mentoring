using Mapster;
using Mentoring.Application.Contracts.Group;
using Mentoring.Application.Contracts.Problem;
using Mentoring.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Mapping;

public class GroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Group, GroupResponse>()
        // استخدام المعامل السحري ?. لحماية الكود من الـ null
            .Map(dest => dest.OwnerName, src => src.Owner != null ? string.Concat(src.Owner.FirstName, " ", src.Owner.LastName) : string.Empty)
            .Map(dest => dest.SubscribersCount, src => src.UserGroups != null ? src.UserGroups.Count() : 0)
            .Map(dest => dest.ProblemsCount, src => src.ProblemGroups != null ? src.ProblemGroups.Count() : 0);

        config.NewConfig< UserGroup , TraineeDataResponse >()
            .Map(dest => dest.Id, src => src.UserId)
            .Map(dest => dest.Name, src => string.Concat(src.User.FirstName, " ", src.User.LastName))
            .Map(dest => dest.Email, src => src.User.Email);


        config.NewConfig<ProblemGroup, ProblemResponse>()
            .Map(dest => dest.Id, src => src.ProblemId)
            .Map(dest => dest.Name, src => src.Problem.Name )
            .Map(dest => dest.Link, src => src.Problem.Link)
            .Map(dest => dest.Notes, src => src.Problem.Notes);

        //config.NewConfig<Group, GroupResponse>()
        //     // دمج الاسم الأول والأخير (مع إضافة مسافة بينهما لشكل أفضل)
        //     .Map(dest => dest.OwnerName, src => string.Concat(src.Owner.FirstName, " ", src.Owner.LastName))

        //     // حساب عدد المشتركين في الجروب
        //     .Map(dest => dest.SubscribersCount, src => src.UserGroups.Count())

        //     // حساب عدد المسائل في الجروب
        //     .Map(dest => dest.ProblemsCount, src => src.ProblemGroups.Count());

        //// ملاحظة: الحقول الأساسية مثل Id, Name, Description, OwnerId 
        //// سيتم عمل Mapping لها تلقائياً لأن الأسماء متطابقة، فلا داعي لكتابتها.
    }
}