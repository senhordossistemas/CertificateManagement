using CertificateManagement.Server.Models;
using CertificateManagement.Server.Properties;
using CertificateManagement.Server.Utilities;

namespace CertificateManagement.Server.Services;

public class CertificateService : ICertificateService
{
    public async Task<CertificateResponse> GenerateCertificateAsync(CertificateRequest request)
    {
        try
        {
            // Generate PDF
            var pdfPath = PdfGenerator.Generate(request);

            // Send Email
            var isEmailSent = await EmailSender.SendEmailAsync(request.Email, pdfPath);

            if (isEmailSent)
            {
                return new CertificateResponse
                {
                    IsSuccess = true,
                    Message = "Certificate successfully generated and emailed."
                };
            }

            return new CertificateResponse
            {
                IsSuccess = false,
                Message = "Failed to send the certificate."
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}