namespace COPDistrictMS.Domain.Entities;

public class Address
{
    public Guid MemberId { get; set; }
    public Member? Member { get; set; }
    public string Residence { get; set; } = String.Empty;
    public string? PostGps { get; set; }
    public string? NearLandmark { get; set; }
    public string? Telephone { get; set; }
}