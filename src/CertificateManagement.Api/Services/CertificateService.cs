using CertificateManagement.Api.Utilities;
using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.CertificateAggregate;
using CertificateManagement.Domain.Models.CertificateAggregate.Dtos;
using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class CertificateService(
    IGenericRepository repository)
    : ICertificateService
{
    public string GenerateCertificateAsync(CertificateRequest request)
    {
        try
        {
            var pdfPath = PdfGenerator.Generate(request);
            return pdfPath;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task CreateCertificate(string pdfPath, User user, Event @event)
    {
        var certificate = new Certificate(pdfPath);
        
        await repository.AddAsync(certificate);

        @event.AddCertificate(certificate.Id);
        user.AddCertificate(certificate.Id);

        await repository.CommitAsync();
    }
}