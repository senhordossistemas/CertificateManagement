using CertificateManagement.Server.Models;

namespace CertificateManagement.Server.Contracts;

public interface ICertificateService
{
    Task<CertificateResponse> GenerateCertificateAsync(CertificateRequest request);
}