using CertificateManagement.Api.Models;
using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.CertificateAggregate;
using CertificateManagement.Domain.Models.CertificateAggregate.Dtos;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
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
        if (user == null)
            return NotFound($"User with ID {userId} not found.");

        var @event = await eventService.Get(eventId);
        if (@event == null)
            return NotFound($"Event with ID {eventId} not found.");

        var response = await GenerateAndSendCertificateAsync(user, @event);
        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("generate/{eventId:int}")]
    public async Task<IActionResult> GenerateCertificatesForEvent(int eventId)
    {
        var @event = await eventService.Get(eventId);
        if (@event == null)
            return NotFound($"Event with ID {eventId} not found.");

        var users = await userService.Get();
        var responses = new List<CertificateResponse>();

        foreach (var user in users)
        {
            var response = await GenerateAndSendCertificateAsync(user, @event);
            responses.Add(response);
        }

        return Ok(responses);
    }

    /// <summary>
    ///     Gera e envia um certificado para um usuário específico de um evento.
    /// </summary>
    /// <param name="user">Usuário para quem o certificado será gerado.</param>
    /// <param name="event">Evento ao qual o certificado está relacionado.</param>
    /// <returns>Resposta com o resultado da operação.</returns>
    private async Task<CertificateResponse> GenerateAndSendCertificateAsync(User user, Event @event)
    {
        try
        {
            // Gerar o PDF do certificado
            var pdfPath = certificateService.GenerateCertificateAsync(new CertificateRequest
            {
                Name = user.Name,
                EventTitle = @event.Name,
                Organization = "Senhor dos Sistemas",
                Hours = 8
            });

            // Criar o registro do certificado
            await certificateService.CreateCertificate(pdfPath, user, @event);

            // Enviar por e-mail
            var isEmailSent = await emailSendingService.SendEmailAsync(user.Email, pdfPath);

            return isEmailSent
                ? new CertificateResponse(true, "Certificado gerado e enviado para o e-mail cadastrado.", user.Id)
                : new CertificateResponse(false, "Certificado gerado, mas não foi possível enviar por e-mail.",
                    user.Id);
        }
        catch (Exception ex)
        {
            // Log de erro (se necessário)
            Console.WriteLine($"Error generating certificate for user {user.Id}: {ex.Message}");
            return new CertificateResponse(false, "Erro ao gerar o certificado.", user.Id);
        }
    }
}