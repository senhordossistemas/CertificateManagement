namespace CertificateManagement.Domain.Models.UserAggregate.Entities;

public class User(string name, string email)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public DateTime RegistrationDate { get; private set; } = DateTime.UtcNow;

    private readonly List<UserCertificate> _certificates = [];
    public IReadOnlyCollection<UserCertificate> Certificates => _certificates.AsReadOnly();

    public void AddCertificate(int certificateId)
    {
        _certificates.Add(new UserCertificate(Id, certificateId));
    }
}