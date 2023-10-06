namespace COPDistrictMS.Domain.Entities;

public class MinistryOffering : Offering
{
    public string Ministry { get; set; } = string.Empty;
    public Guid DistrictId { get; set; }
    public District? District { get; set; }
}
