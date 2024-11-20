using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.EventAggregate.Enums;

namespace CertificateManagement.Domain.Models.EventAggregate.Entities;

public class Event
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public EventStatus Status { get; private set; } = EventStatus.Pending; // Status inicial padrão

    private readonly List<EventCertificate> _certificates = [];
    public IReadOnlyCollection<EventCertificate> Certificates => _certificates.AsReadOnly();
    
    public Event(string name, DateTime startDate, DateTime endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Pending;
    }
    
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
