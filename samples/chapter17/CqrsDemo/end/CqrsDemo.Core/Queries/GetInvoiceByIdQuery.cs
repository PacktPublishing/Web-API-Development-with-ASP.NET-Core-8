using CqrsDemo.Core.Models.Dto;

using MediatR;

namespace CqrsDemo.Core.Queries;
public class GetInvoiceByIdQuery(Guid id) : IRequest<InvoiceDto?>
{
    public Guid Id { get; set; } = id;
}
