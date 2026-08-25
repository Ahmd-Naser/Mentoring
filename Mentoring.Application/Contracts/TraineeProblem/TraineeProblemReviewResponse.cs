using Mentoring.Application.Contracts.Submission;
using Mentoring.Core.Enums;

namespace Mentoring.Application.Contracts.TraineeProblem;

public record TraineeProblemReviewResponse(
    string TraineeId,
    string TraineeName,
    string TraineeEmail,
    ProblemStatus Status,
    int TotalMinutes,
    DateTime? LastStartedAt,
    IEnumerable<SubmissionResponse> Submissions
);