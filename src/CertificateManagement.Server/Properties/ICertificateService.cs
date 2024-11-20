using CertificateManagement.Server.Models;

namespace CertificateManagement.Server.Properties;

public interface ICertificateService
{
    Task<CertificateResponse> GenerateCertificateAsync(CertificateRequest request);
}