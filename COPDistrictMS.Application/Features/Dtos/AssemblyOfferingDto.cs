namespace COPDistrictMS.Application.Features.Dtos;

public class AssemblyOfferingDto
{
    public string OfferingType { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = String.Empty;
}