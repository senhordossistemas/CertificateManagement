namespace CertificateManagement.Domain.Models.UserAggregate.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime RegistrationDate { get; private set; } = DateTime.UtcNow;

    private readonly List<UserCertificate> _certificates = [];
    public IReadOnlyCollection<UserCertificate> Certificates => _certificates.AsReadOnly();
}
