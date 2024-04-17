namespace InvoiceApp.WebApi.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string body);
}
