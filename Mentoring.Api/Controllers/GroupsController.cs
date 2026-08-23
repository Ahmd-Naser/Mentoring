namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupsController(IGroupService groupService) : ControllerBase
{
    private readonly IGroupService _groupService = groupService;

    [HttpGet("me")]
    public async Task<IActionResult> GetAllGroups()
    {
        var userId = User.GetUserId();
        var groups = await _groupService.GetAllAsync(userId!);
        return Ok(groups.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGroupById([FromRoute]int id)
    {
        var result = await _groupService.GetByIdAsync(id);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        
        var ownerId = User.GetUserId();
        
        var result = await _groupService.CreateAsync(request, ownerId!);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetGroupById), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGroup([FromRoute] int id, [FromBody] CreateGroupRequest request)
    {

        var userId = User.GetUserId();  
        var result = await _groupService.UpdateAsync(id, request, userId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] int id)
    {
        var userId = User.GetUserId(); 
        var result = await _groupService.DeleteAsync(id, userId!    );
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpPost("{groupId:int}/trainees")]
    public async Task<IActionResult> AddTraineeToGroup([FromRoute] int groupId, [FromBody] AddTraineeRequest request)
    {
        var requestorId = User.GetUserId()  ; 
        var result = await _groupService.AddTraineeToGroupAsync(groupId, request, requestorId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{groupId:int}/trainees")]
    public async Task<IActionResult> RemoveTraineeFromGroup([FromRoute] int groupId, [FromBody] AddTraineeRequest request)
    {
        var requestorId = User.GetUserId();
        var result = await _groupService.RemoveTraineeFromGroupAsync(groupId, request, requestorId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpGet("{groupId:int}/trainees")]
    public async Task<IActionResult> GetAllTraineesInGroupAsync([FromRoute] int groupId)
    {
        var result = await _groupService.GetAllTraineesInGroupAsync(groupId);

        
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("{groupId:int}/problems/{problemId:int}")]
    public async Task<IActionResult> AddProblemToGroup([FromRoute] int groupId, [FromRoute] int problemId)
    {
        var requestorId = User.GetUserId();
        var result = await _groupService.AddProblemToGroupAsync(groupId, problemId, requestorId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{groupId:int}/problems/{problemId:int}")]
    public async Task<IActionResult> RemoveProblemFromGroup([FromRoute] int groupId, [FromRoute] int problemId)
    {
        var requestorId = User.GetUserId();
        var result = await _groupService.RemoveProblemFromGroupAsync(groupId, problemId, requestorId!);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpGet("{groupId:int}/problems")]
    public async Task<IActionResult> GetAllProblemsInGroup([FromRoute] int groupId)
    {
        var result = await _groupService.GetAllProblemsInGroupAsync(groupId);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

}