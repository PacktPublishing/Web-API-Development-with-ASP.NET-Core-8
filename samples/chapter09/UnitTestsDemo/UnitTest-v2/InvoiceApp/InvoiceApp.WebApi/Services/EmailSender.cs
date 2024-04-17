using System.Net;
using System.Net.Mail;
using InvoiceApp.WebApi.Interfaces;

namespace InvoiceApp.WebApi.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // These configurations can be moved to appsettings.json
        var fromAddress = new MailAddress("from@example.com", "InvoiceApp");
        var fromPassword = "from_password";

        var toAddress = new MailAddress(to);

        using var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        MailMessage message = new(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        await smtp.SendMailAsync(message);
    }
}
