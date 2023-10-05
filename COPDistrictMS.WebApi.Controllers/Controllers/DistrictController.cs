using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;
using COPDistrictMS.Application.Features.Districts.Queries.GetAll;
using COPDistrictMS.Application.Features.Districts.Queries.GetSingle; 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[Route("api/districts")]
[Authorize]
[ApiController]
public class DistrictController : ControllerBase
{
    private readonly IMediator _mediator;

    public DistrictController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse>> GetOwn()
    {
        var result = await _mediator.Send(new GetAllDistrictsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "GetSingleDistrict")]
    public async Task<ActionResult<BaseResponse>> GetSingle([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetSingleDistrictQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse>> Create([FromBody] CreateADistrictCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return CreatedAtAction(nameof(GetSingle), new { id = result.Data }, result);
        }
        
        return BadRequest(result);
    }
}
