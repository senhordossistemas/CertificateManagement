using CertificateManagement.Domain.Models.CertificateAggregate.Dtos;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CertificateManagement.Api.Utilities;

public static class PdfGenerator
{
    static PdfGenerator()
    {
        Settings.License = LicenseType.Community; // se não utiliza linceça paga
    }

    public static string Generate(CertificateRequest request)
    {
        try
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    SetBasicConfiguration(page);

                    page.Content().Column(column =>
                    {
                        SetLogo(column);

                        column.Item().PaddingVertical(10).AlignCenter().Text("CERTIFICADO")
                            .FontSize(28).Bold().FontColor(Colors.Black);

                        SetMainContent(request, column);

                        SetSignatureSpace(column);
                    });
                });
            });

            var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");

            document.GeneratePdf(filePath);

            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #region Private Methods

    private static void SetBasicConfiguration(PageDescriptor page)
    {
        page.Size(PageSizes.A4.Landscape());
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.Black));
    }

    private static void SetLogo(ColumnDescriptor column)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "img", "OIG.jpeg");
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image not found at {imagePath}");

        column.Item().Height(100).AlignCenter().Row(row =>
        {
            row.RelativeItem().Height(90).Width(90).Image(imagePath, ImageScaling.FitWidth);
        });

        column.Item().PaddingBottom(10);
    }

    private static void SetMainContent(CertificateRequest request, ColumnDescriptor column)
    {
        column.Item().PaddingVertical(10).AlignCenter().Text(txt =>
        {
            txt.Span("Certificamos que ").FontSize(16);
            txt.Span(request.Name).Bold().FontSize(18);
            txt.Span(" participou do evento ").FontSize(16);
            txt.Span(request.EventTitle).Bold().FontSize(18);
            txt.Span(", promovido por ").FontSize(16);
            txt.Span(request.Organization).Bold().FontSize(16);
            txt.Span(", realizado no período de ").FontSize(16);
            txt.Span(request.Date.ToString("dd/MM/yyyy")).Bold().FontSize(16);
            txt.Span(", com carga horária de ").FontSize(16);
            txt.Span($"{request.Hours} horas").Bold().FontSize(16);
            txt.Span(".");
        });
    }

    private static void SetSignatureSpace(ColumnDescriptor column)
    {
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
    }

    #endregion
}