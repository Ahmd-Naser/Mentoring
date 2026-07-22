using Mapster;
using Mentoring.Application.Contracts.TraineeProblem;
using Mentoring.Application.Interfaces;
using Mentoring.Core.Abstractions;
using Mentoring.Core.Enums;
using Mentoring.Core.Errors;
using Mentoring.EF.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Services;

public class TraineeProblemService (ApplicationDbContext context): ITraineeProblemService
{
    private readonly ApplicationDbContext _context = context;

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
        var response = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .ProjectToType<TraineeProblemResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if(response is null)
            return Result.Failure<TraineeProblemResponse>( TraineeProblemErrors.NotFound);
        
        return Result.Success(response);
    }
    public async Task<Result<TraineeProblemResponse>> StartProblemAsync(string userId, int groupId, int problemId, CancellationToken cancellationToken = default)
    {
        var groupExists = await _context.Groups
            .AnyAsync(g => g.Id == groupId, cancellationToken);

        if (!groupExists)
            return Result.Failure<TraineeProblemResponse>(GroupErrors.NotFound);

        // 2. التحقق من وجود المسألة أصلاً
        var problemExists = await _context.Problems
            .AnyAsync(p => p.Id == problemId, cancellationToken);

        if (!problemExists)
            return Result.Failure<TraineeProblemResponse>(ProblemErrors.NotFound);

        var isStarted = await _context.TraineeProblems
            .AnyAsync(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId, cancellationToken);

        if(isStarted)
            return Result.Failure<TraineeProblemResponse>(TraineeProblemErrors.AlreadyStarted);

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

        var response = traineeProblem.Adapt<TraineeProblemResponse>();

        return Result.Success(response);

    }

   

    public async Task<Result> UpdateTraineeProblemAsync(string userId, int groupId, int problemId, UpdateTraineeProblemRequest request, CancellationToken cancellationToken = default)
    {
        var traineeProblem = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .FirstOrDefaultAsync(cancellationToken);

        if (traineeProblem is  null)
            return Result.Failure(TraineeProblemErrors.NotFound);

        traineeProblem.Status = request.Status;

        if(_context.Submissions.AnyAsync(s => s.TraineeProblemId == traineeProblem.Id && s.Verdict == SubmissionVerdict.Accepted, cancellationToken).Result)
        {
            traineeProblem.Status = ProblemStatus.Successful;
        }

        if (traineeProblem.LastStartedAt.HasValue)
        {
            var timeSpent = (int)(DateTime.UtcNow - traineeProblem.LastStartedAt.Value).TotalSeconds;
            timeSpent = Math.Min(timeSpent, 45);

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
        var problemExists = await _context.Problems
            .AnyAsync(p => p.Id == problemId, cancellationToken);
        if (!problemExists)
            return Result.Failure<TraineeProblemMinutesResponse>(ProblemErrors.NotFound);


        var totalTimeSpent = await _context.TraineeProblems
            .Where(tp => tp.UserId == userId && tp.GroupId == groupId && tp.ProblemId == problemId)
            .Select(tp => tp.TimeSpentInSeconds)
            .FirstOrDefaultAsync(cancellationToken);

        if (totalTimeSpent == 0)
            return Result.Failure<TraineeProblemMinutesResponse>(TraineeProblemErrors.NotFound);

        var response = new TraineeProblemMinutesResponse(
            totalTimeSpent / 60 // Convert seconds to minutes
        );

        return Result.Success(response);
    }

}
