using Mentoring.Application.Contracts.Group;
using Mentoring.Application.Contracts.Problem;

namespace Mentoring.Application.Services;

public class GroupService(ApplicationDbContext context) : IGroupService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<GroupResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await _context.Groups
        .Where(g => g.OwnerId == userId || g.UserGroups.Any(ug => ug.UserId == userId) )
        .ProjectToType<GroupResponse>()
        .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<GroupResponse>>(response);

    }
    public async Task<Result<GroupResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _context.Groups
        .Where(g => g.Id == id)
        .ProjectToType<GroupResponse>()
        .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result.Failure<GroupResponse>(GroupErrors.NotFound);


        return Result.Success(response);
    }
    public async Task<Result<GroupResponse>> CreateAsync(CreateGroupRequest request, string ownerId, CancellationToken cancellationToken = default)
    {
        var group = request.Adapt<Core.Entities.Group>();

        group.OwnerId = ownerId;

        await _context.AddAsync(group);
        await _context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(group.Id, cancellationToken);
    }

    public async Task<Result> UpdateAsync(int id, CreateGroupRequest request, string userId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if(group is null)
            return Result.Failure(GroupErrors.NotFound);

        if(group.OwnerId != userId)
            return Result.Failure(GroupErrors.Forbidden);

        group = request.Adapt(group);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        if (group.OwnerId != userId)
            return Result.Failure(GroupErrors.Forbidden);

        _context.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
    public async Task<Result> AddTraineeToGroupAsync(int groupId, AddTraineeRequest request, string requestorId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId , cancellationToken);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        if (group.OwnerId != requestorId)
            return Result.Failure(GroupErrors.Forbidden);

        var trainee = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (trainee is null )
            return Result.Failure(UserErrors.NotFound);

        if (await _context.UserGroups.AnyAsync(ug => ug.GroupId == groupId && ug.UserId == trainee.Id , cancellationToken) )
            return Result.Failure(GroupErrors.DuplicatedTrainee);

        await _context.UserGroups.AddAsync(new UserGroup
        {
            GroupId = groupId,
            UserId = trainee.Id,
            Role = GroupRoles.Trainee
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result> RemoveTraineeFromGroupAsync(int groupId, AddTraineeRequest request, string requestorId, CancellationToken cancellationToken = default)
    {

        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        if (group.OwnerId != requestorId)
            return Result.Failure(GroupErrors.Forbidden);

        var trainee = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (trainee is null)
            return Result.Failure(UserErrors.NotFound);

        int deletedRows = await _context.UserGroups
            .Where(ug => ug.GroupId == groupId && ug.UserId == trainee.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (deletedRows == 0)
            return Result.Failure(GroupErrors.NotFoundTraineeInGroup);


        return Result.Success();
    }


    public async Task<Result> AddProblemToGroupAsync(int groupId, int problemId, string requestorId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        if (group.OwnerId != requestorId)
            return Result.Failure(GroupErrors.Forbidden);

        if(!await _context.Problems.AnyAsync(p => p.Id == problemId, cancellationToken))
            return Result.Failure(ProblemErrors.NotFound);

        if (await _context.ProblemGroups.AnyAsync(pg => pg.ProblemId == problemId && pg.GroupId == groupId, cancellationToken))
            return Result.Failure(GroupErrors.DuplicatedProblem);

        await _context.ProblemGroups.AddAsync(new ProblemGroup
            {
                GroupId = groupId,
                ProblemId = problemId

            }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }



    public async Task<Result> RemoveProblemFromGroupAsync(int groupId, int problemId, string requestorId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        if (group.OwnerId != requestorId)
            return Result.Failure(GroupErrors.Forbidden);

        if (!await _context.ProblemGroups.AnyAsync(pg => pg.ProblemId == problemId && pg.GroupId == groupId , cancellationToken))
            return Result.Failure(GroupErrors.NotFoundInGroup);

        await _context.ProblemGroups
            .Where(pg => pg.ProblemId == problemId && pg.GroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<TraineeDataResponse>>> GetAllTraineesInGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        var Trainees = await _context.UserGroups
            .Where(ug => ug.GroupId == groupId && ug.Role == GroupRoles.Trainee)
            .ProjectToType<TraineeDataResponse>()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<TraineeDataResponse> >(Trainees);
    }

    public async Task<Result<IEnumerable<GroupProblemResponse>>> GetAllProblemsInGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        var problems = await _context.ProblemGroups
            .Where(pg => pg.GroupId == groupId)
            .Select(pg => new GroupProblemResponse(
                pg.ProblemId,
                pg.Problem.Name,
                pg.Problem.Link,
                pg.Problem.Difficulty,
                pg.Deadline
            ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return Result.Success<IEnumerable<GroupProblemResponse>>(problems);
    }
}
