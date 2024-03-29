﻿using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;
using COPDistrictMS.Application.Features.Districts.Commands.DeleteDistrict;
using COPDistrictMS.Application.Features.Districts.Queries.GetAll;
using COPDistrictMS.Application.Features.Districts.Queries.GetSingle;
using COPDistrictMS.Application.Features.Offerings.Commands.AddMinistryOffering;
using COPDistrictMS.Application.Features.Offerings.Queries.GetMinistryOfferings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[Authorize]
[ApiController]
[Route("api/districts")]
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

    [HttpGet("{districtId:guid}/ministries/offerings")]
    public async Task<ActionResult<BaseResponse>> GetMinistryOffering([FromRoute] Guid districtId,
        [FromQuery] string ministry, [FromQuery] int page = 1, [FromQuery] int size = 20)
    {
        var response = await _mediator.Send(new GetMinistryOfferingsQuery(districtId, page, size, ministry));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<BaseResponse>> Create([FromBody] CreateADistrictCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return CreatedAtAction(nameof(GetSingle), new { id = result.Data }, result);
        }
        
        return BadRequest(result);
    }

    [HttpPost("{districtId:guid}/ministries/offerings")]
    public async Task<ActionResult<BaseResponse>> AddMinistryOffering([FromRoute] Guid districtId,
        [FromBody] AddMinistryOfferingCommand command)
    {
        var response = await _mediator.Send(command);

        if (!response.Success)
            return BadRequest(response);
        
        return Accepted(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<BaseResponse>> Delete([FromRoute] Guid id)
    {
        _= await _mediator.Send(new DeleteDistrictCommand(id));
        return NoContent();
    }
}
