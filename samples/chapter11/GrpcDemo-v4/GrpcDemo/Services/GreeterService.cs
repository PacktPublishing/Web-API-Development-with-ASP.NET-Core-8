using Grpc.Core;

namespace GrpcDemo.Services;

public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        //var updateInvoiceDueDateRequest = new UpdateInvoiceDueDateRequest
        //{
        //    InvoiceId = "3193C36C-2AAB-49A7-A0B1-6BDB3B69DEA1",
        //    DueDate = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(30)),
        //    GracePeriod = Duration.FromTimeSpan(TimeSpan.FromDays(10))
        //};

        //var dueDate = updateInvoiceDueDateRequest.DueDate.ToDateTime();
        //var gracePeriod = updateInvoiceDueDateRequest.GracePeriod.ToTimeSpan();

        // var updateIds = new UpdateInvoicesStatusRequest();
        // updateIds.InvoiceIds.Add(new[]
        //     { "99143291-2523-4EE8-8A4D-27B09334C980", "BB4E6CFE-6AAE-4948-941A-26D1FBF59E8A" });

        var updateInvoicesStatusRequest = new UpdateInvoicesStatusRequest();
        // Add one key-value pair
        updateInvoicesStatusRequest.InvoiceStatusMap.Add("3193C36C-2AAB-49A7-A0B1-6BDB3B69DEA1", InvoiceStatus.AwaitPayment);
        // Add multiple key-value pairs
        updateInvoicesStatusRequest.InvoiceStatusMap.Add(new Dictionary<string, InvoiceStatus>
        {
            { "99143291-2523-4EE8-8A4D-27B09334C980", InvoiceStatus.Paid },
            { "BB4E6CFE-6AAE-4948-941A-26D1FBF59E8A", InvoiceStatus.Overdue }
        });

        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
