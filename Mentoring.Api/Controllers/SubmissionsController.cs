using Mentoring.Application.Contracts.Submission;
using Mentoring.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubmissionsController(ISubmissionService submissionService) : ControllerBase
{
    private readonly ISubmissionService _submissionService = submissionService;

    [HttpGet("trainee-problem/{traineeProblemId}")]
    public async Task<IActionResult> GetAll([FromRoute]int traineeProblemId)
    {
        var result = await _submissionService.GetAllForTraineeProblemAsync(traineeProblemId);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem(); 
    }

    [HttpGet("trainee-problem/{traineeProblemId}/submission/{submissionId}")]
    public async Task<IActionResult> GetSubmission([FromRoute] int traineeProblemId, [FromRoute] int submissionId)
    {
        var result = await _submissionService.GetByIdAsync(traineeProblemId, submissionId);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();

    }

    [HttpPost("submission")]
    public async Task<IActionResult> AddSubmission( [FromBody] SubmissionRequest request)
    {
        var result = await _submissionService.CreateSubmissionAsync( request);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("submission/{id}")]
    public async Task<IActionResult> UpdateSubmission([FromRoute] int id , [FromBody] SubmissionRequest request)
    {
        var result = await _submissionService.UpdateSubmissionAsync(id , request);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();

    }

    [HttpDelete("submission/{id}")]
    public async Task<IActionResult> DeleteSubmission([FromRoute] int id)
    {
        var result = await _submissionService.DeleteSubmissionAsync(id);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

}
