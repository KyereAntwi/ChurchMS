namespace COPDistrictMS.Domain.Entities;

public class District : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string Area { get; set; } = String.Empty;
    public string? DistrictPastorFullName { get; set; }

    public ICollection<Assembly> Assemblies { get; set; } = default!;
}