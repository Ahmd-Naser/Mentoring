using Mentoring.Application.Contracts.Group;
using Mentoring.Application.Contracts.Problem;

namespace Mentoring.Application.Interfaces;

public interface IGroupService
{
    // 1. الأساسيات (CRUD Operations)
    Task<Result<GroupResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<GroupResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken = default);

    Task<Result<GroupResponse>> CreateAsync(CreateGroupRequest request, string ownerId, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int id, CreateGroupRequest request, string userId, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);

    // 2. العمليات الخاصة ببيئة العمل (Business Logic)
    Task<Result> AddTraineeToGroupAsync(int groupId, AddTraineeRequest request , string requestorId, CancellationToken cancellationToken = default);

    Task<Result> RemoveTraineeFromGroupAsync(int groupId, AddTraineeRequest request, string requestorId, CancellationToken cancellationToken = default);

    Task<Result> AddProblemToGroupAsync(int groupId, int problemId, string requestorId, CancellationToken cancellationToken = default);

    Task<Result> RemoveProblemFromGroupAsync(int groupId, int problemId, string requestorId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<TraineeDataResponse>>> GetAllTraineesInGroupAsync(int groupId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<GroupProblemResponse>>> GetAllProblemsInGroupAsync(int groupId, CancellationToken cancellationToken = default);
}