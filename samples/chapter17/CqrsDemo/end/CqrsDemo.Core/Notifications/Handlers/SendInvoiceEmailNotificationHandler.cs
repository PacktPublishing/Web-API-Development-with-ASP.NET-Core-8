using CqrsDemo.Core.Services.Interfaces;

using MediatR;

namespace CqrsDemo.Core.Notifications.Handlers;
public class SendInvoiceEmailNotificationHandler(IInvoiceService invoiceService) : INotificationHandler<SendInvoiceNotification>
{
    public async Task Handle(SendInvoiceNotification notification, CancellationToken cancellationToken)
    {
        // Send email notification
        var invoice = await invoiceService.GetAsync(notification.InvoiceId, cancellationToken);
        if (invoice is null || string.IsNullOrWhiteSpace(invoice.ContactEmail))
        {
            return;
        }
        // Send email notification
        Console.WriteLine($"Sending email notification to {invoice.ContactEmail} for invoice {invoice.Id}");
    }
}
