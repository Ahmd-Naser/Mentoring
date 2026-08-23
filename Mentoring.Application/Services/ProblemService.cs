using Mentoring.Application.Contracts.Problem;

namespace Mentoring.Application.Services;

public class ProblemService(ApplicationDbContext context) : IProblemService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Result<IEnumerable<ProblemResponse>>> GetAllProblemsAsync(string userId)
    {
        var response = await _context.Problems
            .Where(p => p.CreatedById == userId)
            .ProjectToType<ProblemResponse>()
            .ToListAsync();

        return Result.Success<IEnumerable<ProblemResponse>>(response);
    }

    public async Task<Result<ProblemResponse>> GetProblemByIdAsync(int problemId, string userId)
    {
        var response = await _context.Problems
            .Where(p => p.Id == problemId && p.CreatedById == userId)
            .ProjectToType<ProblemResponse>() 
            .FirstOrDefaultAsync();

        if(response is null) 
            return Result.Failure<ProblemResponse>(ProblemErrors.NotFound);

        return Result.Success(response);

    }

    public async Task<Result<ProblemResponse>> CreateProblemAsync(string userId, CreateProblemRequest request)
    {
        var problem = request.Adapt<Problem>();
        problem.CreatedById = userId;

        await _context.Problems.AddAsync(problem);
        await _context.SaveChangesAsync();

        var response = problem.Adapt<ProblemResponse>();

        return Result.Success(response);
    }

    public async Task<Result> UpdateProblemAsync(int problemId , string userId ,CreateProblemRequest request)
    {
        var problem = await _context.Problems.FirstOrDefaultAsync(p => p.Id == problemId);

        if(problem is null)
            return Result.Failure(ProblemErrors.NotFound);

        if(userId != problem.CreatedById)
            return Result.Failure(ProblemErrors.Forbidden);

        problem = request.Adapt(problem);

        await _context.SaveChangesAsync();

        return Result.Success();
    }
    public async Task<Result> DeleteProblemAsync(int problemId , string userId)
    {
        var ownerId = await _context.Problems
            .Where(p => p.Id == problemId)
            .Select(p => p.CreatedById)
            .FirstOrDefaultAsync();

        if (ownerId is null)
            return Result.Failure(ProblemErrors.NotFound);

        if (userId != ownerId)
            return Result.Failure(ProblemErrors.Forbidden);

        await _context.Problems
            .Where(p => p.Id == problemId)
            .ExecuteDeleteAsync();

        return Result.Success();
    }

}
