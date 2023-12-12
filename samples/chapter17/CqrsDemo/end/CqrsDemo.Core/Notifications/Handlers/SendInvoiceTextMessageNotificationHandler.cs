using CqrsDemo.Core.Services.Interfaces;

using MediatR;

namespace CqrsDemo.Core.Notifications.Handlers;
public class SendInvoiceTextMessageNotificationHandler(IInvoiceService invoiceService) : INotificationHandler<SendInvoiceNotification>
{
    public async Task Handle(SendInvoiceNotification notification, CancellationToken cancellationToken)
    {
        // Send text message notification
        var invoice = await invoiceService.GetAsync(notification.InvoiceId, cancellationToken);
        if (invoice is null || string.IsNullOrWhiteSpace(invoice.ContactPhone))
        {
            return;
        }
        // Send text message notification
        Console.WriteLine($"Sending text message notification to {invoice.ContactPhone} for invoice {invoice.Id}");
    }
}