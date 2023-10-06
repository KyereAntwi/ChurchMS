using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Members.Commands.AddAMember;
using COPDistrictMS.Application.Features.Members.Commands.DeleteMember;
using COPDistrictMS.Application.Features.Members.Commands.UpdateMember;
using COPDistrictMS.Application.Features.Members.Queries.FilterMembersList;
using COPDistrictMS.Application.Features.Members.Queries.GetMemberDetails;
using COPDistrictMS.Application.Features.Members.Queries.GetMembersOfAssembly;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[Authorize]
[ApiController]
[Route("api/districts/{districtId:guid}")]
public class MemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse>> Get(
        [FromQuery] Guid assemblyId,
        [FromQuery] DateOnly dateOfBirth,
        [FromQuery] int monthOfBirth,
        [FromQuery] int yearOfBirth,
        [FromRoute] Guid districtId,
        [FromQuery] int page = 1,
        [FromQuery] int size = 20,
        [FromQuery] string nameString = "",
        [FromQuery] string gender = "")
    {
        var response = await _mediator.Send(new FilterMembersListQuery(districtId, nameString, gender, assemblyId,
            dateOfBirth, monthOfBirth, yearOfBirth, page, size));

        return Ok(response);
    }

    [HttpGet("members/{memberId:guid}")]
    public async Task<ActionResult<BaseResponse>> Get([FromRoute] Guid memberId)
    {
        var response = await _mediator.Send(new GetMemberDetailsQuery(memberId));
        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("assemblies/{assemblyId:guid}/members")]
    public async Task<ActionResult<BaseResponse>> GetAssemblyMembers([FromRoute] Guid assemblyId, [FromQuery] int page = 1, 
        [FromQuery] int size = 20)
    {
        var response = await _mediator.Send(new GetMembersOfAssemblyQuery(assemblyId, page, size));
        return Ok(response);
    }

    [HttpPost("members")]
    public async Task<ActionResult<BaseResponse>> AddNew([FromBody] AddAMemberCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
            return CreatedAtAction(nameof(Get), new { memberId = result.Data }, result);
        return BadRequest(result);
    }

    [HttpDelete("members/{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteMemberCommand(id));
        return NoContent();
    }

    [HttpPut("members/{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Update([FromRoute] Guid id, [FromBody] UpdateMemberCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Success)
            return Accepted(id);
        return BadRequest(result);
    }
}
