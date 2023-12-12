using AutoMapper;

using CqrsDemo.Core.Models;
using CqrsDemo.Core.Models.Dto;
using CqrsDemo.Core.Repositories;
using CqrsDemo.Core.Services.Interfaces;

namespace CqrsDemo.Core.Services.Implementations;
public class InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper) : IInvoiceService
{
    public async Task<InvoiceDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var invoice = await invoiceRepository.GetAsync(id, cancellationToken);
        return invoice == null ? null : mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<List<InvoiceWithoutItemsDto>> GetAllListAsync(CancellationToken cancellationToken = default)
    {
        var invoices = await invoiceRepository.GetAllListAsync(cancellationToken);
        return mapper.Map<List<InvoiceWithoutItemsDto>>(invoices);
    }

    public async Task<List<InvoiceWithoutItemsDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var invoices = await invoiceRepository.GetPagedListAsync(pageIndex, pageSize, cancellationToken);
        return mapper.Map<List<InvoiceWithoutItemsDto>>(invoices);
    }

    public async Task<InvoiceDto> AddAsync(CreateOrUpdateInvoiceDto invoice, CancellationToken cancellationToken = default)
    {
        var invoiceEntity = mapper.Map<Invoice>(invoice);
        // Do some business logic here, e.g. calculate total amount, assign invoice number, etc.
        invoiceEntity.Id = Guid.NewGuid();
        invoiceEntity.InvoiceItems.ForEach(x =>
        {
            x.InvoiceId = invoiceEntity.Id;
            x.Amount = x.Quantity * x.UnitPrice;
        });
        invoiceEntity.Amount = invoiceEntity.InvoiceItems.Sum(x => x.Amount);
        // Generate invoice number using the date and time of creation, and the invoice id. Make it unique and short.
        invoiceEntity.InvoiceNumber = $"{DateTime.UtcNow:yyyyMMddHHmmss}-{invoiceEntity.Id.ToString()[..8]}";

        var result = await invoiceRepository.AddAsync(invoiceEntity, cancellationToken);
        return mapper.Map<InvoiceDto>(result);
    }

    public async Task<InvoiceDto?> UpdateAsync(Guid id, CreateOrUpdateInvoiceDto invoice, CancellationToken cancellationToken = default)
    {
        var invoiceEntity = mapper.Map<Invoice>(invoice);
        invoiceEntity.Id = id;
        invoiceEntity.InvoiceItems.ForEach(x =>
        {
            x.InvoiceId = invoiceEntity.Id;
            x.Amount = x.Quantity * x.UnitPrice;
        });
        invoiceEntity.Amount = invoiceEntity.InvoiceItems.Sum(x => x.Amount);
        var result = await invoiceRepository.UpdateAsync(invoiceEntity, cancellationToken);
        return result == null ? null : mapper.Map<InvoiceDto>(result);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return invoiceRepository.DeleteAsync(id, cancellationToken);
    }
}
