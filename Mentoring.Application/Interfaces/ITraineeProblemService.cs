using Mentoring.Application.Contracts.TraineeProblem;

namespace Mentoring.Application.Interfaces;

public interface ITraineeProblemService
{

    // 1. Read (Single & List)
    Task<Result<IEnumerable<TraineeProblemResponse>>> GetTraineeProblemsByGroupAsync(string userId, int groupId, CancellationToken cancellationToken = default);
    Task<Result<TraineeProblemResponse>> GetTraineeProblemAsync(string userId,int groupId , int problemId, CancellationToken cancellationToken);
    Task<Result<TraineeProblemResponse>> GetTraineeProblemByIdAsync(string userId, int traineeProblemId, CancellationToken cancellationToken = default);

    Task<Result<TraineeProblemMinutesResponse>> TotalMinutesSpentForSpecificProblem(string userId, int groupId, int problemId, CancellationToken cancellationToken = default);

    Task<Result<TraineeProblemResponse>> StartProblemAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken = default);
    // 3. Update / Submit Solution
    Task<Result> UpdateTraineeProblemAsync(string userId, int groupId, int problemId, UpdateTraineeProblemRequest request, CancellationToken cancellationToken = default);

    // 4. Delete / Unassign (مع التحقق من منفذ الطلب)
    Task<Result> DeleteTraineeProblemAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken = default);

}
