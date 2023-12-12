using AutoMapper;

using CqrsDemo.Core.Models;
using CqrsDemo.Core.Models.Dto;

namespace CqrsDemo.Core;
public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<CreateOrUpdateInvoiceItemDto, InvoiceItem>();
        CreateMap<InvoiceItem, InvoiceItemDto>();
        CreateMap<CreateOrUpdateInvoiceDto, Invoice>();
        CreateMap<Invoice, InvoiceWithoutItemsDto>();
        CreateMap<Invoice, InvoiceDto>();
    }
}
