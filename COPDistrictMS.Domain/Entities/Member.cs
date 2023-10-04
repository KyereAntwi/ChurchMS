namespace COPDistrictMS.Domain.Entities;

public class Member : BaseEntity
{
    public string LastName { get; set; } = String.Empty;
    public string GivingName { get; set; } = String.Empty;
    public string? OtherNames { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Uri? PhotographUrl { get; set; }
    public string Gender { get; set; } = String.Empty;

    public Assembly? Assembly { get; set; }
    public Address? Address { get; set; }
    public OfficerType? OfficerType { get; set; }
}