namespace CertificateManagement.Domain.Models.UserAggregate.Entities;

public class UserCertificate
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int CertificateId { get; private set; }
}