using CertificateManagement.Api.Configurations;
using CertificateManagement.Domain.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CertificateManagement.Api.Services;

public class EmailSendingService : IEmailSendingService
{
    private static EmailSettings _emailSettings;

    public EmailSendingService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(string recipientEmail, string pdfPath)
    {
        try
        {
            var message = SetBasicInfo(recipientEmail, out var body);

            SetMimePartBody(pdfPath, body, message);

            await SendMessage(message);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    #region Private Methods

    private static MimeMessage SetBasicInfo(string recipientEmail, out TextPart body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        message.To.Add(new MailboxAddress("Recipient", recipientEmail));
        message.Subject = "Your Certificate";

        body = new TextPart("plain")
        {
            Text = "Please find your certificate attached."
        };
        return message;
    }

    private static void SetMimePartBody(string pdfPath, TextPart body, MimeMessage message)
    {
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(File.OpenRead(pdfPath)),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName(pdfPath)
        };

        var multipart = new Multipart("mixed")
        {
            body,
            attachment
        };

        message.Body = multipart;
    }

    private static async Task SendMessage(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    #endregion
}