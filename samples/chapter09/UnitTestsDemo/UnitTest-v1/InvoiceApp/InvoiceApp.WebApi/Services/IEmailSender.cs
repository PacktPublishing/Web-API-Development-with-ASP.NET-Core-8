namespace InvoiceApp.WebApi.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string body);
}
