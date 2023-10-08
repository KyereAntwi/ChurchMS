namespace COPDistrictMS.Domain.Entities;

public class OfficersMeetingAttachment : Attachment
{
    public Guid MeetingId { get; set; }
    public OfficersMeeting? OfficersMeeting { get; set; }
}