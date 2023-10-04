namespace COPDistrictMS.Application.Features.Dtos;

public class MemberDto
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = String.Empty;
    public string GivingName { get; set; } = String.Empty;
    public string? OtherNames { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Uri? PhotographUrl { get; set; }
    public string Gender { get; set; } = String.Empty;

    public AssemblyDto? Assembly { get; set; }
    public AddressDto? Address { get; set; }
    public string? OfficerType { get; set; }
    public string FullName => $"{LastName}, {GivingName} {OtherNames}";
}
