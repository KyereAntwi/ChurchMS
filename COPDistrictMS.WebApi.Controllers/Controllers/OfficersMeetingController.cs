using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/districts/{districtId:guid}/officers/meetings")]
public class OfficersMeetingController : ControllerBase
{
    private readonly IMediator _mediator;

    public OfficersMeetingController(IMediator mediator)
    {
        _mediator = mediator;
    }
}