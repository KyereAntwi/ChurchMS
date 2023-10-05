namespace COPDistrictMS.Application.Commons;

public record AuthenticationRequest(
    string Email,
    string Password
    );
