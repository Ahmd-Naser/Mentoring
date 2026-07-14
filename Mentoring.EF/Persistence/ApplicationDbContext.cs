using Mentoring.Core.Entities;
using Mentoring.EF.Persistence.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Mentoring.EF.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Group> Groups { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<TraineeProblem> TraineeProblems { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<ProblemGroup> ProblemGroups { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // مهم جداً أن تبقى هذه في البداية


        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
