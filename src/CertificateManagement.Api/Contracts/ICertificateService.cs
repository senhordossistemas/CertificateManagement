using CertificateManagement.Api.Models;

namespace CertificateManagement.Api.Contracts;

public interface ICertificateService
{
    Task<CertificateResponse> GenerateCertificateAsync(CertificateRequest request);
}