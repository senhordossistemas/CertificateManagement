namespace CertificateManagement.Domain.Contracts;

public interface IEmailSendingService
{
    Task<bool> SendEmailAsync(string recipientEmail, string pdfPath);
}