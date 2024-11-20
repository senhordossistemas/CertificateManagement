namespace CertificateManagement.Domain.Models.CertificateAggregate.Entities;

public class Certificate(string filePath)
{
    public int Id { get; private set; }
    public string FilePath { get; private set; } = filePath;

    public DateTime GeneratedDate { get; private set; } = DateTime.UtcNow;
}