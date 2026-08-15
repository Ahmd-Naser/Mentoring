
using Mentoring.Application.Contracts.Submission;

namespace Mentoring.Application.Interfaces;

public interface ISubmissionService
{
    Task<Result<IEnumerable<SubmissionResponse>>> GetAllForTraineeProblemAsync(int traineeProblemId, CancellationToken cancellationToken = default);
    Task<Result<SubmissionResponse>> GetByIdAsync(int traineeProblemId ,int id, CancellationToken cancellationToken = default);
    Task<Result<SubmissionResponse>> CreateSubmissionAsync(int traineeProblemId, SubmissionRequest request, CancellationToken cancellationToken = default);
    Task<Result<SubmissionResponse>> UpdateSubmissionAsync(int  id, SubmissionRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteSubmissionAsync(int id, CancellationToken cancellationToken = default);
}
