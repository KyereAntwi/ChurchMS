using COPDistrictMS.Application.Features.Districts.Queries.GetAll;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Persistence.Repositories;
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
    public async Task<ActionResult<BaseRepository<List<DistrictDto>>>> GetOwn()
    {
        var result = await _mediator.Send(new GetAllDistrictsQuery());
        return Ok(result);
    }
}
