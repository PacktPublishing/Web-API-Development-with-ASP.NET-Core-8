namespace InvoiceApp.WebApi;

public enum InvoiceStatus
{
    Draft,
    AwaitPayment,
    Paid,
    Overdue,
    Cancelled
}

