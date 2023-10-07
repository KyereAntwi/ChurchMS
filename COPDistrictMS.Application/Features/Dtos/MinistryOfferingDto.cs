namespace COPDistrictMS.Application.Features.Dtos;

public class MinistryOfferingDto
{
    public string Ministry { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = String.Empty;
}