using Mentoring.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mentoring.Application.Contracts.Group;

namespace Mentoring.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController(IGroupService groupService) : ControllerBase
{
    private readonly IGroupService _groupService = groupService;

    [HttpGet("")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService.GetAllAsync();
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
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var ownerId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        
        var result = await _groupService.CreateAsync(request, ownerId);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetGroupById), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGroup([FromRoute] int id, [FromBody] CreateGroupRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.UpdateAsync(id, request, userId);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] int id)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var userId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.DeleteAsync(id, userId);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpPost("{groupId:int}/trainees")]
    public async Task<IActionResult> AddTraineeToGroup([FromRoute] int groupId, [FromBody] AddTraineeRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var requestorId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.AddTraineeToGroupAsync(groupId, request, requestorId);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{groupId:int}/trainees")]
    public async Task<IActionResult> RemoveTraineeFromGroup([FromRoute] int groupId, [FromBody] AddTraineeRequest request)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var requestorId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.RemoveTraineeFromGroupAsync(groupId, request, requestorId);
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
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var requestorId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.AddProblemToGroupAsync(groupId, problemId, requestorId);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{groupId:int}/problems/{problemId:int}")]
    public async Task<IActionResult> RemoveProblemFromGroup([FromRoute] int groupId, [FromRoute] int problemId)
    {
        // Assuming you have a way to get the current user's ID, e.g., from claims
        var requestorId = "b9e76e00-6f22-4003-b7a3-c6cde7244966"; // Replace "sub" with the appropriate claim type
        var result = await _groupService.RemoveProblemFromGroupAsync(groupId, problemId, requestorId);
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