using CqrsDemo.Core.Models.Dto;

namespace CqrsDemo.Core.Services.Interfaces;
public interface IInvoiceService
{
    Task<InvoiceDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<InvoiceWithoutItemsDto>> GetAllListAsync(CancellationToken cancellationToken = default);
    Task<List<InvoiceWithoutItemsDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<InvoiceDto> AddAsync(CreateOrUpdateInvoiceDto invoice, CancellationToken cancellationToken = default);
    Task<InvoiceDto?> UpdateAsync(Guid id, CreateOrUpdateInvoiceDto invoice, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
