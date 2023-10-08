namespace COPDistrictMS.Domain.Entities;

public class Attachment
{
    public Guid Id { get; set; }
    public string? FileType { get; set; }
    public Uri? FileUri { get; set; }
    public string? Description { get; set; }
}