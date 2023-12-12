using CqrsDemo.Core.Models.Dto;
using CqrsDemo.Core.Services.Interfaces;

using MediatR;

namespace CqrsDemo.Core.Queries.Handlers;
public class GetInvoiceListQueryHandler(IInvoiceService invoiceService) : IRequestHandler<GetInvoiceListQuery, List<InvoiceWithoutItemsDto>>
{
    public Task<List<InvoiceWithoutItemsDto>> Handle(GetInvoiceListQuery request, CancellationToken cancellationToken)
    {
        return invoiceService.GetPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
    }
}