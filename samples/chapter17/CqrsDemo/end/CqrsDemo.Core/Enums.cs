namespace CqrsDemo.Core;

public enum InvoiceStatus
{
    Draft,
    AwaitPayment,
    Paid,
    Overdue,
    Cancelled
}
