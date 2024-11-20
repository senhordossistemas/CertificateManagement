using CertificateManagement.Api.Contracts;
using CertificateManagement.Api.Models;
using CertificateManagement.Api.Utilities;
using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController(
    ICertificateService certificateService,
    IUserService userService,
    IEventService eventService) : ControllerBase
{
    [HttpPost("complete/{userId:int}/{eventId:int}")]
    public async Task<IActionResult> CompleteCourse(int userId, int eventId)
    {
        var user = await userService.Get(userId);
        var @event = await eventService.Get(eventId);

        var pdfPath = certificateService.GenerateCertificateAsync(new CertificateRequest
        {
            Name = user.Name,
            EventTitle = @event.Name,
            Organization = "Senhor dos Sistemas",
            Hours = 8
        });

        var certificate = new Certificate(pdfPath);

        await certificateService.AddAsync(certificate);

        @event.AddCertificate(certificate.Id);

        var isEmailSent = await EmailSender.SendEmailAsync(user.Email, pdfPath);

        if (isEmailSent)
            return Ok(new CertificateResponse(true, "Certificado Gerado e enviado para o email cadastrado", user.Id));

        return BadRequest(new CertificateResponse(true, "Certificado não gerado", user.Id));
    }

    [HttpPost("generate/{eventId:int}")]
    public async Task<IActionResult> GenerateCertificate(int eventId)
    {
        var @event = await eventService.Get(eventId);
        
        if (@event == null)
            return NotFound("Event not found.");

        var users = await userService.Get();

        var result = new List<CertificateResponse>();
        
        foreach (var user in users)
        {
            var pdfPath = certificateService.GenerateCertificateAsync(new CertificateRequest
            {
                Name = user.Name,
                EventTitle = @event.Name,
                Organization = "Senhor dos Sistemas",
                Hours = 8
            });

            var certificate = new Certificate(pdfPath);

            await certificateService.AddAsync(certificate);

            @event.AddCertificate(certificate.Id);

            var isMailSent = await EmailSender.SendEmailAsync(user.Email, pdfPath);

            result.Add(isMailSent
                ? new CertificateResponse(true, "Certificado Gerado e enviado para o email cadastrado", user.Id)
                : new CertificateResponse(true, "Certificado não gerado", user.Id));
        }

        return Ok(result);
    }
}