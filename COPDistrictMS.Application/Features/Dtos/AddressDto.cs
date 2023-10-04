namespace COPDistrictMS.Application.Features.Dtos;

public class AddressDto
{
    public string Residence { get; set; } = String.Empty;
    public string? PostGps { get; set; }
    public string? NearLandmark { get; set; }
    public string? Telephone { get; set; }
}
