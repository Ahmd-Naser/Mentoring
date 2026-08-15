using Mentoring.Application.Contracts.Problem;

namespace Mentoring.Application.Interfaces;

public interface IProblemService
{
    Task<Result<IEnumerable<ProblemResponse>>> GetAllProblemsAsync(string userId);
    Task<Result<ProblemResponse>> GetProblemByIdAsync(int problemId, string userId);

    Task<Result<ProblemResponse>> CreateProblemAsync(string userId, CreateProblemRequest request);

    Task<Result> UpdateProblemAsync(int problemId, string userId, CreateProblemRequest request);

    Task<Result> DeleteProblemAsync(int problemId, string userId);


}
