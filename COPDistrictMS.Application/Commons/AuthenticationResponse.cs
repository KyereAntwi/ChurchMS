﻿namespace COPDistrictMS.Application.Commons;

public class AuthenticationResponse
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string? Role { get; set; }
}
