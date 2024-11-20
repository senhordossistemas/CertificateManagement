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
        {
            return Ok(new CertificateResponse
            {
                IsSuccess = true,
                Message = "O certificado foi gerado e enviado para o seu email cadastrado."
            });
        }

        return BadRequest(new CertificateResponse
        {
            IsSuccess = false,
            Message = "Erro ao gerar ou enviar o certificado para o e-mail"
        });
    }

    [HttpPost("generate")]
    public IActionResult GenerateCertificate([FromQuery] string email)
    {
        var request = new CertificateRequest()
        {
            Email = email
        };

        var response = certificateService.GenerateCertificateAsync(request);

        return Ok(response);
    }
}