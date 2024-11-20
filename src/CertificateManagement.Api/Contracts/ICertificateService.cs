using CertificateManagement.Api.Models;
using CertificateManagement.Domain.Models.CertificateAggregate.Entities;

namespace CertificateManagement.Api.Contracts;

public interface ICertificateService
{
    Task AddAsync(Certificate certificate);
    Task<List<Certificate>> Get();
    Task<Certificate> Get(int id);
    string GenerateCertificateAsync(CertificateRequest request);
}