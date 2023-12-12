using CqrsDemo.Core.Models.Dto;
using CqrsDemo.Core.Services.Interfaces;

using MediatR;

namespace CqrsDemo.Core.Queries.Handlers;
public class GetInvoiceByIdQueryHandler(IInvoiceService invoiceService) : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
{
    public Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        return invoiceService.GetAsync(request.Id, cancellationToken);
    }
}
