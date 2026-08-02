using MapsterMapper;
using Mentoring.Application.Interfaces;
using Mentoring.Application.Services;
using Mentoring.Core.Entities;
using Mentoring.Core.Settings;
using Mentoring.EF.Authentication;
using Mentoring.EF.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Mentoring.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMapster();

        services.AddValidators();

        services.AddAuthConfig(configuration);



        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<ITraineeProblemService, TraineeProblemService>();
        services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<IAuthService, AuthService>();


        return services;

    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        // 1. قراءة وتسجيل جميع الفاليديتورز الموجودة في مشروع Application دفعة واحدة
        services.AddValidatorsFromAssemblyContaining<Application.Contracts.Group.CreateGroupRequestValidator>();

        // 2. تفعيل الـ Auto Validation لتعترض الطلبات الخاطئة قبل وصولها للـ Controllers
        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        // 1. استدعاء الإعدادات العامة لـ Mapster
        var config = TypeAdapterConfig.GlobalSettings;

        // 2. توجيه Mapster لعمل مسح (Scan) للمشروع الذي يحتوي على الإعدادات
        // نستخدم أي كلاس موجود داخل Mentoring.Application كدليل للوصول للـ Assembly الخاص به
        config.Scan(typeof(Application.Mapping.GroupMappingConfig).Assembly);
        // ملاحظة: يمكنك استبدال GlobalUsings باسم أي كلاس آخر داخل التطبيق مثل GroupMappingConfig

        // 3. تسجيل إعدادات Mapster في الـ Dependency Injection (مهم جداً)
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            // إعدادات كلمات المرور (اختياري)
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };

        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }



}
