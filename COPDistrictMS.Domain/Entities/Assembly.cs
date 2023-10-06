namespace COPDistrictMS.Domain.Entities;

public class Assembly : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string Location { get; set; } = String.Empty;
    public string? YearEstablished { get; set; }

    public Guid DistrictId { get; set; }
    public District? District { get; set; }

    // public ICollection<Member> Members { get; set; } = default!;
    public ICollection<Manager> Managers { get; set; } = default!;
    public ICollection<AssemblyOffering> Offerings { get; set; } = default!;
    public AssemblyPresiding? PresidingOfficer { get; set; }
}