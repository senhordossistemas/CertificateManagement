namespace CertificateManagement.Server.Models;

public class CertificateRequest
{
    public string Email { get; set; }
    public string Name { get; set; } = "Inocêncio Quissumua Cardoso";
    public string EventTitle { get; set; } = "I Semana Global de Empreendedorismo";
    public string Organization { get; set; } = "Núcleo de Empreendedorismo";
    public DateTime EventDate { get; set; } = DateTime.Now;
    public int Hours { get; set; } = 8;
}