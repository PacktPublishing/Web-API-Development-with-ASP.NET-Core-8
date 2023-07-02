using InvoiceApp.WebApi.Models;

namespace InvoiceApp.WebApi.Interfaces;

public interface IEmailService
{
    (string to, string subject, string body) GenerateInvoiceEmail(Invoice invoice);
    Task SendEmailAsync(string to, string subject, string body);
}
