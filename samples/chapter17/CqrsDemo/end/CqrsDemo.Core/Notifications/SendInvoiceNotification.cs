using MediatR;

namespace CqrsDemo.Core.Notifications;
public class SendInvoiceNotification(Guid invoiceId) : INotification
{
    public Guid InvoiceId { get; set; } = invoiceId;
}
