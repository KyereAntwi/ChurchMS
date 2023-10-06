namespace COPDistrictMS.Domain.Entities;

public class AssemblyOffering : Offering
{
    public Guid AssemblyId { get; set; }
    public Assembly? Assembly { get; set; }
    public string OfferingType { get; set; } = string.Empty;
}
