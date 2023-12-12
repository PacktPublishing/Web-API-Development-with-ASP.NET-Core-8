using CqrsDemo.Core.Models.Dto;

using MediatR;

namespace CqrsDemo.Core.Commands;
public class CreateInvoiceCommand(CreateOrUpdateInvoiceDto invoice) : IRequest<InvoiceDto>
{
    public CreateOrUpdateInvoiceDto Invoice { get; set; } = invoice;
}
