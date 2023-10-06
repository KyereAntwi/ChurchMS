using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Assemblies.Commands.AddManagersToAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.CreateAnAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.CreateMultipleAssemblies;
using COPDistrictMS.Application.Features.Assemblies.Commands.DeleteAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.UpdateAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.UpdatePresidingOfficer;
using COPDistrictMS.Application.Features.Assemblies.Queries.GetAssemblyDetails;
using COPDistrictMS.Application.Features.Assemblies.Queries.GetManagedAssemblies;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[Authorize]
[ApiController]
[Route("api/districts/{districtId:guid}/assemblies")]
public class AssemblyController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssemblyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse>> GetManaged()
    {
        var result = await _mediator.Send(new GetManagedAssembliesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetAssemblyDetailsQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse>> Create([FromRoute] Guid districtId, [FromBody] CreateAssemblyDto request)
    {
        var command = new CreateAnAssemblyCommand(request.Title, request.Location, request.YearEstablished!, districtId);
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return CreatedAtAction(nameof(Get), new
            {
                districtId,
                id = result.Data
            }, result);
        }

        return BadRequest(result);
    }

    [HttpPost("multiple")]
    public async Task<ActionResult<BaseResponse>> CreateMultiple([FromRoute] Guid districtId,
        [FromBody] List<CreateAssemblyDto> requests)
    {
        
        var command = new CreateMultipleAssembliesCommand()
        {
            DistrictId = districtId,
            CreateAssemblyDtos = requests
        };
        
        // command.CreateAssemblyDtos = requests;
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return CreatedAtAction(nameof(Get), new
            {
                districtId,
                id = result.Data
            }, result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Update([FromBody] UpdateAssemblyCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Success)
        {
            return Accepted(result);
        }

        return BadRequest(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Delete([FromRoute] Guid id)
    {
        _ = await _mediator.Send(new DeleteAssemblyCommand(id));
        return NoContent();
    }

    [HttpPost("{assemblyId:guid}/presiding/{memberId:guid}")]
    public async Task<ActionResult<BaseResponse>> UpdatePresidingLeader([FromRoute] Guid assemblyId, [FromRoute] Guid memberId)
    {
        var result = await _mediator.Send(new UpdatePresidingOfficerCommand(assemblyId, memberId));
        return Accepted(result);
    }

    [HttpPost("{assemblyId:guid}/manage")]
    public async Task<ActionResult<BaseResponse>> UpdateManagersList([FromRoute] Guid assemblyId, [FromBody] List<string> managersUsernames)
    {
        var result = await _mediator.Send(
            new AddManagersToAssemblyCommand { AssemblyId = assemblyId, ManageUsernames = managersUsernames });

        return Accepted(result);
    }
}