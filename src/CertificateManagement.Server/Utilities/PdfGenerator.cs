using CertificateManagement.Server.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CertificateManagement.Server.Utilities;

public static class PdfGenerator
{
    static PdfGenerator()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }
    
    public static string Generate(CertificateRequest request)
    {
        var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");

        try
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.Black));
                    
                    page.Content().Column(column =>
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "img", "OIG.jpeg");
                        if (!File.Exists(imagePath))
                        {
                            throw new FileNotFoundException($"Image not found at {imagePath}");
                        }
                        
                        // Logo e Título
                        column.Item().Height(100).AlignCenter().Row(row =>
                        {
                            row.RelativeItem().Height(90).Width(90).Image(imagePath, ImageScaling.FitWidth);
                        });
                        
                        column.Item().PaddingBottom(10);
                        
                        column.Item().PaddingVertical(10).AlignCenter().Text("CERTIFICADO")
                            .FontSize(28).Bold().FontColor(Colors.Black);
                        
                        // Conteúdo Principal
                        column.Item().PaddingVertical(10).AlignCenter().Text(txt =>
                        {
                            txt.Span("Certificamos que ").FontSize(16);
                            txt.Span(request.Name).Bold().FontSize(18);
                            txt.Span(" participou do evento ").FontSize(16);
                            txt.Span(request.EventTitle).Bold().FontSize(18);
                            txt.Span(", promovido por ").FontSize(16);
                            txt.Span(request.Organization).Bold().FontSize(16);
                            txt.Span(", realizado no período de ").FontSize(16);
                            txt.Span(request.EventDate.ToString("dd/MM/yyyy")).Bold().FontSize(16);
                            txt.Span(", com carga horária de ").FontSize(16);
                            txt.Span($"{request.Hours} horas").Bold().FontSize(16);
                            txt.Span(".");
                        });
                        
                        // Espaço para assinaturas
                        column.Item().PaddingTop(50).Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("Prof. Ma. Telma").Bold();
                                col.Item().AlignCenter().Text("Coordenadora do Núcleo");
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("Prof. Me. Julio").Bold();
                                col.Item().AlignCenter().Text("Diretor da Unidade de Ciências");
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("Prof. Dra. Venancia").Bold();
                                col.Item().AlignCenter().Text("Pró-Reitora de Pós-Graduação, Pesquisa e Extensão");
                            });
                        });
                    });
                    
                });
            });

            document.GeneratePdf(filePath);

            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}