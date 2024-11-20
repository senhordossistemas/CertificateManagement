using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController(IEventService eventService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] Event eventEntity)
    {
        await eventService.AddAsync(eventEntity);
        return Ok(eventEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await eventService.Get();
        return Ok(events);
    }
}
