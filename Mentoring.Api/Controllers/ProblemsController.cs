
using Mentoring.Application.Contracts.Problem;
using Mentoring.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProblemsController(IProblemService problemService) : ControllerBase
{
    private readonly IProblemService _problemService = problemService;

    [HttpGet("")]
    public async Task<IActionResult> GetAllProblems()
    {
        var result = await _problemService.GetAllProblemsAsync();

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProblemById([FromRoute] int id)
    {
        var result = await _problemService.GetProblemByIdAsync(id);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProblem([FromBody] CreateProblemRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _problemService.CreateProblemAsync(userId, request);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetProblemById), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProblem([FromRoute] int id, [FromBody] CreateProblemRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _problemService.UpdateProblemAsync(id, userId, request);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProblem([FromRoute] int id)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _problemService.DeleteProblemAsync(id, userId);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

}
