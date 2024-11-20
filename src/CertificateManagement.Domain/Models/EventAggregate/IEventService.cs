using CertificateManagement.Domain.Models.EventAggregate.Entities;

namespace CertificateManagement.Domain.Models.EventAggregate;

public interface IEventService
{
    Task AddAsync(Event @event);
    Task<List<Event>> Get();
    Task<Event> Get(int id);
}