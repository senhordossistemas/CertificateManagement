using CertificateManagement.Api.Models;
using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.CertificateAggregate;
using CertificateManagement.Domain.Models.CertificateAggregate.Dtos;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController(
    ICertificateService certificateService,
    IUserService userService,
    IEventService eventService,
    IEmailSendingService emailSendingService) : ControllerBase
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

        await certificateService.CreateCertificate(pdfPath, user, @event);
        
        var isEmailSent = await emailSendingService.SendEmailAsync(user.Email, pdfPath);

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

            await certificateService.CreateCertificate(pdfPath, user, @event);

            var isMailSent = await emailSendingService.SendEmailAsync(user.Email, pdfPath);

            result.Add(isMailSent
                ? new CertificateResponse(true, "Certificado Gerado e enviado para o email cadastrado", user.Id)
                : new CertificateResponse(true, "Certificado não gerado", user.Id));
        }

        return Ok(result);
    }
}