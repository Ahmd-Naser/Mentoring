using Mentoring.Application.Contracts.TraineeProblem;
using Mentoring.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TraineeProblemsController(ITraineeProblemService traineeProblemService) : ControllerBase
{
    private readonly ITraineeProblemService _traineeProblemService = traineeProblemService;

    [HttpGet("group/{groupId}")]
    public async Task<IActionResult> GetTraineeProblems([FromRoute] int groupId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type

        var traineeProblems = await _traineeProblemService.GetTraineeProblemsByGroupAsync(userId , groupId, cancellationToken: default);
        return Ok(traineeProblems.Value);
    }

    [HttpGet("group/{groupId}/problem/{problemId}")]
    public async Task<IActionResult> GetTraineeProblem([FromRoute] int groupId, [FromRoute] int problemId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type

        var result = await _traineeProblemService.GetTraineeProblemAsync(userId, groupId, problemId, cancellationToken: default);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();

    }

    [HttpGet("group/{groupId}/problem/{problemId}/total-minutes")]
    public async Task<IActionResult> GetTotalMinutesSpent([FromRoute] int groupId, [FromRoute] int problemId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _traineeProblemService.TotalMinutesSpentForSpecificProblem(userId, groupId, problemId, cancellationToken: default);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("trainee-problem/{traineeProblemId}")]
    public async Task<IActionResult> GetTraineeProblemById([FromRoute] int traineeProblemId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _traineeProblemService.GetTraineeProblemByIdAsync(userId, traineeProblemId, cancellationToken: default);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("group/{groupId}/problem/{problemId}/start")]
    public async Task<IActionResult> StartProblem([FromRoute] int groupId, [FromRoute] int problemId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _traineeProblemService.StartProblemAsync(userId, groupId, problemId, cancellationToken: default);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("group/{groupId}/problem/{problemId}")]
    public async Task<IActionResult> CompleteProblem([FromRoute]int groupId, [FromRoute] int problemId , [FromBody]UpdateTraineeProblemRequest request)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _traineeProblemService
            .UpdateTraineeProblemAsync(userId, groupId, problemId, request, cancellationToken: default);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("group/{groupId}/problem/{problemId}")]
    public async Task<IActionResult> DeleteProblem([FromRoute]int groupId, [FromRoute] int problemId)
    {
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _traineeProblemService.DeleteTraineeProblemAsync(userId, groupId, problemId, cancellationToken: default);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    
}
