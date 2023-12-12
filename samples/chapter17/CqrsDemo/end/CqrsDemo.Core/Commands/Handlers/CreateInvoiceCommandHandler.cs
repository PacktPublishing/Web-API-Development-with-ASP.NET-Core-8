using CqrsDemo.Core.Models.Dto;
using CqrsDemo.Core.Services.Interfaces;

using MediatR;

namespace CqrsDemo.Core.Commands.Handlers;
public class CreateInvoiceCommandHandler(IInvoiceService invoiceService) : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    public Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        return invoiceService.AddAsync(request.Invoice, cancellationToken);
    }
}
