namespace COPDistrictMS.Application.Features.Districts.Dtos;

public class DistrictDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string? DistrictPastorFullName { get; set; }
    public IEnumerable<Guid> LocalAssembliesIds { get; set; } = new List<Guid>();
}
