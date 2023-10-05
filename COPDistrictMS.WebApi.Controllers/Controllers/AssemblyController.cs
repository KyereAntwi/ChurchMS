using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Assemblies.Commands.CreateAnAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.CreateMultipleAssemblies;
using COPDistrictMS.Application.Features.Assemblies.Commands.DeleteAssembly;
using COPDistrictMS.Application.Features.Assemblies.Commands.UpdateAssembly;
using COPDistrictMS.Application.Features.Assemblies.Queries.GetAssemblyDetails;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
}