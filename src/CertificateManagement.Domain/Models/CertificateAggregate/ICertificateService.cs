using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.Dtos;

namespace CertificateManagement.Domain.Models.CertificateAggregate;

public interface ICertificateService
{
    Task AddAsync(Certificate certificate);
    Task<List<Certificate>> Get();
    Task<Certificate> Get(int id);
    string GenerateCertificateAsync(CertificateRequest request);
}