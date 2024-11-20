using CertificateManagement.Api.Contracts;
using CertificateManagement.Api.Models;
using CertificateManagement.Api.Utilities;
using CertificateManagement.Data;
using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class CertificateService(DataContext context) : ICertificateService
{
    public async Task AddAsync(Certificate certificate)
    {
        await context.Certificates.AddAsync(certificate);
        await context.SaveChangesAsync();
    }

    public async Task<List<Certificate>> Get() => await context.Certificates.ToListAsync();

    public async Task<Certificate> Get(int id) => await context.Certificates.FirstOrDefaultAsync(e => e.Id == id);

    public async Task Commit() => await context.SaveChangesAsync();
    
    public  string GenerateCertificateAsync(CertificateRequest request)
    {
        try
        {
            // Generate PDF
            var pdfPath = PdfGenerator.Generate(request);
            return pdfPath;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}