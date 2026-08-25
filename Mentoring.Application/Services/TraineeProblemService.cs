using Mentoring.Application.Contracts.Submission;
using Mentoring.Application.Contracts.TraineeProblem;

namespace Mentoring.Application.Services;

public class TraineeProblemService (ApplicationDbContext context): ITraineeProblemService
{
    private readonly ApplicationDbContext _context = context;

    private const int MaxTimeSpentPerSessionInSeconds = 45 * 60;

    public async Task<Result<IEnumerable<TraineeProblemResponse>>> GetTraineeProblemsByGroupAsync(string userId, int groupId, CancellationToken cancellationToken = default)
    {
        
        var response = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId)
            .ProjectToType<TraineeProblemResponse>()
            .ToListAsync(cancellationToken);
        
        return Result.Success<IEnumerable<TraineeProblemResponse>>(response) ;


    }

    public async Task<Result<TraineeProblemResponse>> GetTraineeProblemAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken)
    {

        var groupExists = await _context.Groups
            .AnyAsync(g => g.Id == groupId, cancellationToken);

        if (!groupExists)
            return Result.Failure<TraineeProblemResponse>(GroupErrors.NotFound);

        // 2. التحقق من وجود المسألة أصلاً
        var problemExists = await _context.ProblemGroups
            .AnyAsync(p => p.ProblemId == problemId && p.GroupId == groupId, cancellationToken);

        if (!problemExists)
            return Result.Failure<TraineeProblemResponse>(ProblemErrors.NotFound);

        TraineeProblemResponse response = default!;

        if(!await _context.TraineeProblems.AnyAsync(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId, cancellationToken)){
            var traineeProblem = new TraineeProblem
            {
                UserId = userId,
                GroupId = groupId,
                ProblemId = problemId,
                Status = ProblemStatus.InProgress,
                LastStartedAt = DateTime.UtcNow
            };

            await _context.TraineeProblems.AddAsync(traineeProblem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        
        
        }

        response = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .ProjectToType<TraineeProblemResponse>()
            .FirstAsync(cancellationToken);

        return Result.Success(response) ;
    }
    public async Task<Result> StartProblemToggleAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken = default)
    {
       

        var traineeProblem = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .FirstOrDefaultAsync(cancellationToken);

        if (traineeProblem is null)
            return Result.Failure<TraineeProblemResponse>(TraineeProblemErrors.NotFound);

        if( traineeProblem.LastStartedAt.HasValue)
        {
            var timeSpent = (int)(DateTime.UtcNow - traineeProblem.LastStartedAt.Value).TotalSeconds;
            timeSpent = Math.Min(timeSpent, MaxTimeSpentPerSessionInSeconds);
            traineeProblem.TimeSpentInSeconds += timeSpent;
            traineeProblem.LastStartedAt = null;
        }
        else
        {
            traineeProblem.LastStartedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);


        return Result.Success();

    }

   

    public async Task<Result> UpdateTraineeProblemAsync(string userId, int groupId, int problemId, UpdateTraineeProblemRequest request, CancellationToken cancellationToken = default)
    {
        var traineeProblem = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .FirstOrDefaultAsync(cancellationToken);

        if (traineeProblem is  null)
            return Result.Failure(TraineeProblemErrors.NotFound);

        traineeProblem.Status = request.Status;

        if(await _context.Submissions.AnyAsync(s => s.TraineeProblemId == traineeProblem.Id && s.Verdict == SubmissionVerdict.Accepted, cancellationToken))
        {
            traineeProblem.Status = ProblemStatus.Successful;
        }

        if (traineeProblem.LastStartedAt.HasValue)
        {
            var timeSpent = (int)(DateTime.UtcNow - traineeProblem.LastStartedAt.Value).TotalSeconds;
            timeSpent = Math.Min(timeSpent, MaxTimeSpentPerSessionInSeconds);

            traineeProblem.TimeSpentInSeconds += timeSpent;
            traineeProblem.LastStartedAt = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();


    }
    public async Task<Result> DeleteTraineeProblemAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken = default)
    {
       

        // 2. الحذف المباشر بدون تحميل الكيان في الذاكرة (Performance-First)
        var rowsAffected = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .ExecuteDeleteAsync(cancellationToken);

        // 3. إذا لم يتم حذف أي صف، فهذا يعني أن العنصر غير موجود أصلاً
        if (rowsAffected == 0)
            return Result.Failure(TraineeProblemErrors.NotFound);

        return Result.Success();
    }

    public async Task<Result<TraineeProblemResponse>> GetTraineeProblemByIdAsync(string userId, int traineeProblemId, CancellationToken cancellationToken = default)
    {
        var response = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.Id == traineeProblemId)
            .ProjectToType<TraineeProblemResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result.Failure<TraineeProblemResponse>(TraineeProblemErrors.NotFound);

        return Result.Success(response);
    }

    public async Task<Result<TraineeProblemMinutesResponse> > TotalMinutesSpentForSpecificProblem(string userId, int groupId, int problemId, CancellationToken cancellationToken = default)
    {
        var groupExists = await _context.Groups
            .AnyAsync(g => g.Id == groupId, cancellationToken);

        if (!groupExists)
            return Result.Failure<TraineeProblemMinutesResponse>(GroupErrors.NotFound);

        // 2. التحقق من وجود المسألة أصلاً
        var problemExists = await _context.ProblemGroups
            .AnyAsync(pg => pg.ProblemId == problemId && pg.GroupId == groupId, cancellationToken);

        if (!problemExists)
            return Result.Failure<TraineeProblemMinutesResponse>(ProblemErrors.NotFound);


        var traineeProblem = await _context.TraineeProblems
         .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
         .FirstOrDefaultAsync(cancellationToken);

        if (traineeProblem is null)
            return Result.Failure<TraineeProblemMinutesResponse>(TraineeProblemErrors.NotFound);


        // 🌟 حساب الثواني المخزنة + ثواني الجلسة المفتوحة حالياً
        var totalSeconds = traineeProblem.TimeSpentInSeconds;

        if (traineeProblem.LastStartedAt.HasValue)
        {
            var currentSessionSeconds = (int)(DateTime.UtcNow - traineeProblem.LastStartedAt.Value).TotalSeconds;
            totalSeconds += Math.Min(currentSessionSeconds, MaxTimeSpentPerSessionInSeconds);
        }

        var response = new TraineeProblemMinutesResponse(
            totalSeconds / 60 // Convert seconds to minutes
        );

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<TraineeProblemReviewResponse>>> GetProblemReviewsAsync(
    string mentorId,
    int groupId,
    int problemId,
    CancellationToken cancellationToken = default)
    {
        // 1. التحقق من أن المستخدم الحالي هو مالك المجموعة (Mentor)
        var isOwner = await _context.Groups
            .AnyAsync(g => g.Id == groupId && g.OwnerId == mentorId, cancellationToken);

        if (!isOwner)
            return Result.Failure<IEnumerable<TraineeProblemReviewResponse>>(GroupErrors.Forbidden);

        // 2. جلب جميع المتدربين في المجموعة مع بيانات حلولهم لهذه المسألة
        var trainees = await _context.UserGroups
            .Where(gu => gu.GroupId == groupId)
            .Select(gu => gu.User)
            .ToListAsync(cancellationToken);

        var traineeProblems = await _context.TraineeProblems
            .Where(tp => tp.GroupId == groupId && tp.ProblemId == problemId)
            .Include(tp => tp.Submissions)
            .ToListAsync(cancellationToken);

        var response = new List<TraineeProblemReviewResponse>();

        foreach (var trainee in trainees)
        {
            var tp = traineeProblems.FirstOrDefault(x => x.UserId == trainee.Id);

            var totalSeconds = tp?.TimeSpentInSeconds ?? 0;
            if (tp?.LastStartedAt.HasValue == true)
            {
                var sessionSec = (int)(DateTime.UtcNow - tp.LastStartedAt.Value).TotalSeconds;
                totalSeconds += Math.Min(sessionSec, MaxTimeSpentPerSessionInSeconds);
            }

            var submissions = tp?.Submissions?
                .OrderByDescending(s => s.SubmittedAt)
                .Select(s => s.Adapt<SubmissionResponse>())
                .ToList() ?? new List<SubmissionResponse>();

            response.Add(new TraineeProblemReviewResponse(
                TraineeId: trainee.Id,
                TraineeName: $"{trainee.FirstName} {trainee.LastName}".Trim(),
                TraineeEmail: trainee.Email!,
                Status: tp?.Status ?? ProblemStatus.NotOpened,
                TotalMinutes: totalSeconds / 60,
                LastStartedAt: tp?.LastStartedAt,
                Submissions: submissions
            ));
        }

        return Result.Success<IEnumerable<TraineeProblemReviewResponse>>(response);
    }

}
