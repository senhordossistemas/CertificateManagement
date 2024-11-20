using CertificateManagement.Data;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class EventService(DataContext context) : IEventService
{
    public async Task AddAsync(Event @event)
    {
        await context.Events.AddAsync(@event);
        await context.SaveChangesAsync();
    }

    public async Task<List<Event>> Get() => await context.Events.ToListAsync();

    public async Task<Event> Get(int id) => await context.Events.FirstOrDefaultAsync(e => e.Id == id);
}