using CqrsDemo.Core.Models;

namespace CqrsDemo.Core.Repositories;
public interface IInvoiceRepository
{
    Task<Invoice?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Invoice>> GetAllListAsync(CancellationToken cancellationToken);
    Task<List<Invoice>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Invoice?> AddAsync(Invoice invoice, CancellationToken cancellationToken);
    Task<Invoice?> UpdateAsync(Invoice invoice, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
