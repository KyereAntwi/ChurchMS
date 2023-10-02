namespace COPDistrictMS.Application.Features.Districts.Queries.GetAll;

public class DistrictDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Area { get; set; } = String.Empty;
    public string? DistrictPastorFullName { get; set; }
    public IEnumerable<Guid> LocalAssembliesIds { get; set; } = new List<string>();
}
