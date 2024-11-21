namespace CertificateManagement.Api.Configurations;

public record EmailSettings
{
    public string SenderName { get; init; }
    public string SenderEmail { get; init; }
    public string SmtpServer { get; init; }
    public int Port { get; init; }
    public string Password { get; init; }
}