using CertificateManagement.Domain.Models.CertificateAggregate.Dtos;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate.Entities;

namespace CertificateManagement.Domain.Models.CertificateAggregate;

public interface ICertificateService
{
    string GenerateCertificateAsync(CertificateRequest request);
    Task CreateCertificate(string pdfPath, User user, Event @event);
}