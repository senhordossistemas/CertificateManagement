using CertificateManagement.Domain.Models.EventAggregate.Enums;

namespace CertificateManagement.Domain.Models.EventAggregate.Entities;

public class Event(string name, DateTime startDate, DateTime endDate, EventStatus status)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public DateTime StartDate { get; private set; } = startDate;
    public DateTime EndDate { get; private set; } = endDate;
    public EventStatus Status { get; private set; } = status;

    private readonly List<EventCertificate> _certificates = [];
    public IReadOnlyCollection<EventCertificate> Certificates => _certificates.AsReadOnly();

    public void CompleteEvent()
    {
        if (Status == EventStatus.Disabled)
            throw new InvalidOperationException("Cannot complete a disabled event.");

        Status = EventStatus.Completed;
    }

    public void DisableEvent()
    {
        Status = EventStatus.Disabled;
    }

    public void AddCertificate(int certificateId)
    {
        if (Status == EventStatus.Disabled)
            throw new InvalidOperationException("Cannot add certificates to a disabled event.");

        _certificates.Add(new EventCertificate(Id, certificateId));
    }
}