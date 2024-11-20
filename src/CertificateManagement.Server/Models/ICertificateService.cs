namespace CertificateManagement.Server.Models;

public interface ICertificateService
{
    Task<CertificateResponse> GenerateCertificateAsync(CertificateRequest request);
}