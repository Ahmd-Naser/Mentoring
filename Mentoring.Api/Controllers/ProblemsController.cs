
using Mentoring.Application.Contracts.Problem;


namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProblemsController(IProblemService problemService) : ControllerBase
{
    private readonly IProblemService _problemService = problemService;

    [HttpGet("")]
    public async Task<IActionResult> GetAllProblems()
    {
        var userId = User.GetUserId();
        var result = await _problemService.GetAllProblemsAsync(userId!);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProblemById([FromRoute] int id)
    {
        var userId = User.GetUserId();
        var result = await _problemService.GetProblemByIdAsync(id, userId!);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProblem([FromBody] CreateProblemRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = User.GetUserId();
        var result = await _problemService.CreateProblemAsync(userId!, request);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetProblemById), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProblem([FromRoute] int id, [FromBody] CreateProblemRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = User.GetUserId();
        var result = await _problemService.UpdateProblemAsync(id, userId!, request);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProblem([FromRoute] int id)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = User.GetUserId();
        var result = await _problemService.DeleteProblemAsync(id, userId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

}
