using InvoiceApp.WebApi.Models;

namespace InvoiceApp.WebApi.Services;

public class EmailService : IEmailService
{

    public (string to, string subject, string body) GenerateInvoiceEmail(Invoice invoice)
    {
        var to = invoice.Contact.Email;
        var subject = $"Invoice {invoice.InvoiceNumber} for {invoice.Contact.FirstName} {invoice.Contact.LastName}";
        var body = $"""
            Dear {invoice.Contact.FirstName} {invoice.Contact.LastName},

            Thank you for your business. Here are your invoice details:
            Invoice Number: {invoice.InvoiceNumber}
            Invoice Date: {invoice.InvoiceDate.LocalDateTime.ToShortDateString()}
            Invoice Amount: {invoice.Amount.ToString("C")}
            Invoice Items:
            {string.Join(Environment.NewLine, invoice.InvoiceItems.Select(i => $"{i.Description} - {i.Quantity} x {i.UnitPrice.ToString("C")}"))}

            Please pay by {invoice.DueDate.LocalDateTime.ToShortDateString()}. Thank you!

            Regards,
            InvoiceApp
            """;
        return (to, subject, body);
    }

    public Task SendEmailAsync(string to, string subject, string body)
    {
        // Mock the email sending process
        // In real world, you may use a third-party email service, such as SendGrid, MailChimp, Azure Logic Apps, etc.
        return Task.Delay(100);
    }
}