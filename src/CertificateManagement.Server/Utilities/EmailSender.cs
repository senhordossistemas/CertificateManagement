using MailKit.Net.Smtp;
using MimeKit;

namespace CertificateManagement.Server.Utilities;

public static class EmailSender
{
    public static async Task<bool> SendEmailAsync(string recipientEmail, string pdfPath)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Certificate Management", "10enraska@gmail.com"));
            message.To.Add(new MailboxAddress("To Name", recipientEmail));
            message.Subject = "Your Certificate";

            var body = new TextPart("plain")
            {
                Text = "Please find your certificate attached."
            };

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

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("10enraska@gmail.com", "rfkpdfpqlxfyhaag");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}