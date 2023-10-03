namespace COPDistrictMS.Application.Features.Dtos;

public class AssemblyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string YearEstablished { get; set; } = string.Empty;
    public DistrictDto? District { get; set; }
};
