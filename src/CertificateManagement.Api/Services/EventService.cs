using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class EventService(IGenericRepository repository) : IEventService
{
    public async Task AddAsync(Event @event)
    {
        await repository.AddAsync(@event);
        await repository.CommitAsync();
    }

    public async Task<List<Event>> Get() => await repository.Query<Event>().ToListAsync();

    public async Task<Event> Get(int id) => await repository.Query<Event>().FirstOrDefaultAsync(e => e.Id == id);
}