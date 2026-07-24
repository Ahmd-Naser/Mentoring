using Mapster;
using Mentoring.Core.Errors;
using Mentoring.EF.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Services;

public class SubmissionService(ApplicationDbContext context) : ISubmissionService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<SubmissionResponse>>> GetAllForTraineeProblemAsync(int traineeProblemId, CancellationToken cancellationToken = default)
    {
        var isTraineeProblemExists = await _context.TraineeProblems.AnyAsync(tp => tp.Id == traineeProblemId, cancellationToken);
        if (!isTraineeProblemExists) 
            return Result.Failure<IEnumerable<SubmissionResponse>>(TraineeProblemErrors.NotFound);


        var response = await _context.Submissions
            .Where(s => s.TraineeProblemId == traineeProblemId)
            .ProjectToType<SubmissionResponse>()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<SubmissionResponse>>(response);

    }

    public async Task<Result<SubmissionResponse>> GetByIdAsync( int traineeProblemId, int id, CancellationToken cancellationToken = default)
    {
        var isTraineeProblemExists = await _context.TraineeProblems.AnyAsync(tp => tp.Id == traineeProblemId, cancellationToken);
        if (!isTraineeProblemExists)
            return Result.Failure<SubmissionResponse>(TraineeProblemErrors.NotFound);


        var response = await _context.Submissions
            .Where(s => s.Id == id && s.TraineeProblemId == traineeProblemId)
            .ProjectToType<SubmissionResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if(response is null)
            return Result.Failure<SubmissionResponse>(SubmissionErrors.NotFound);


        return Result.Success<SubmissionResponse>(response);


    }
    public async Task<Result<SubmissionResponse>> CreateSubmissionAsync( SubmissionRequest request, CancellationToken cancellationToken = default)
    {
        var isTraineeProblemExist = await _context.TraineeProblems.AnyAsync(tp => tp.Id == request.TraineeProblemId);

        if(!isTraineeProblemExist)
            return Result.Failure<SubmissionResponse>(TraineeProblemErrors.NotFound);

        var isExist = await _context.Submissions.AnyAsync(s => s.CodeLink == request.CodeLink, cancellationToken);

        if (isExist)
            return Result.Failure<SubmissionResponse>(SubmissionErrors.Duplicated);

        var submission = request.Adapt<Submission>();

        await _context.Submissions.AddAsync(submission);
    
        await _context.SaveChangesAsync(cancellationToken);

        var response = submission.Adapt<SubmissionResponse>();

        return Result.Success(response);
    
    }

    public async Task<Result<SubmissionResponse>> UpdateSubmissionAsync(int id, SubmissionRequest request,CancellationToken cancellationToken = default!)
    {
        var submission = await _context.Submissions.Where(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);

        if (submission is null)
            return Result.Failure<SubmissionResponse>(SubmissionErrors.NotFound);

        submission = request.Adapt(submission);

        await _context.SaveChangesAsync(cancellationToken);

        var response = submission.Adapt<SubmissionResponse>();

        return Result.Success(response);
    }

    public async Task<Result> DeleteSubmissionAsync(int id, CancellationToken cancellationToken = default)
    {

        var deletedRows = await _context.Submissions
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync( cancellationToken);

        if(deletedRows == 0)
            return Result.Failure(SubmissionErrors.NotFound);


        return Result.Success();
    }


}
