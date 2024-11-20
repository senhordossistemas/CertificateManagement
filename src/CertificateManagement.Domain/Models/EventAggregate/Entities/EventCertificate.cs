namespace CertificateManagement.Domain.Models.EventAggregate.Entities;

public class EventCertificate(int eventId, int certificateId)
{
    public int Id { get; set; }
    public int EventId { get; set; } = eventId;
    public int CertificateId { get; set; } = certificateId;
}