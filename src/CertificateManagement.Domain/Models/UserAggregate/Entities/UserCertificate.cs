namespace CertificateManagement.Domain.Models.UserAggregate.Entities;

public class UserCertificate(int userId, int certificateId)
{
    public int Id { get; private set; }
    public int UserId { get; private set; } = userId;
    public int CertificateId { get; private set; } = certificateId;
}