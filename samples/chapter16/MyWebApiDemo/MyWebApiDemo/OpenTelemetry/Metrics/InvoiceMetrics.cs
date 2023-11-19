using System.Diagnostics.Metrics;

namespace MyWebApiDemo.OpenTelemetry.Metrics;

public class InvoiceMetrics
{
    private readonly Counter<long> _invoiceCreateCounter;
    private readonly Counter<long> _invoiceReadCounter;
    private readonly Counter<long> _invoiceUpdateCounter;
    private readonly Counter<long> _invoiceDeleteCounter;

    private readonly UpDownCounter<long> _invoiceRequestUpDownCounter;
    private readonly Histogram<double> _invoiceRequestDurationHistogram;
    public InvoiceMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("MyWebApiDemo.Invoice");
        _invoiceCreateCounter = meter.CreateCounter<long>("mywebapidemo.invoices.created");
        _invoiceReadCounter = meter.CreateCounter<long>("mywebapidemo.invoices.read");
        _invoiceUpdateCounter = meter.CreateCounter<long>("mywebapidemo.invoices.updated");
        _invoiceDeleteCounter = meter.CreateCounter<long>("mywebapidemo.invoices.deleted");

        _invoiceRequestUpDownCounter = meter.CreateUpDownCounter<long>("mywebapidemo.invoices.requests");
        _invoiceRequestDurationHistogram = meter.CreateHistogram<double>("mywebapidemo.invoices.request_duration");
    }

    public void IncrementCreate()
    {
        _invoiceCreateCounter.Add(1);
    }

    public void IncrementRead()
    {
        _invoiceReadCounter.Add(1);
    }

    public void IncrementUpdate()
    {
        _invoiceUpdateCounter.Add(1);
    }

    public void IncrementDelete()
    {
        _invoiceDeleteCounter.Add(1);
    }

    public void IncrementRequest()
    {
        _invoiceRequestUpDownCounter.Add(1);
    }

    public void DecrementRequest()
    {
        _invoiceRequestUpDownCounter.Add(-1);
    }

    public void RecordRequestDuration(double duration)
    {
        _invoiceRequestDurationHistogram.Record(duration);
    }
}
