using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;

namespace COPDistrictMS.WebApi.Controllers.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync([FromBody] AuthenticationRequest request)
    {
        return Ok(await _authenticationService.AuthenticateAsync(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync([FromBody] RegistrationRequest request)
    {
        return Ok(await _authenticationService.RegisterAsync(request));
    }
}
