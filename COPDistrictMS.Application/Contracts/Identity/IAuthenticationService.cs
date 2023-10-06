using COPDistrictMS.Application.Commons;

namespace COPDistrictMS.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    Task<RegistrationResponse> RegisterAdminsAsync(RegistrationRequest request);
}
