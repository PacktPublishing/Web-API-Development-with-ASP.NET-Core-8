using InvoiceApp.WebApi.Models;

namespace InvoiceApp.WebApi.Interfaces;

public interface IInvoiceRepository
{
    Task<Invoice?> GetInvoiceAsync(Guid id);
    Task<IEnumerable<Invoice>> GetInvoicesAsync(int page = 1, int pageSize = 10, InvoiceStatus? status = null);
    Task<IEnumerable<Invoice>> GetInvoicesByContactIdAsync(Guid contactId, int page = 1, int pageSize = 10, InvoiceStatus? status = null);
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> UpdateInvoiceAsync(Invoice invoice);
    Task DeleteInvoiceAsync(Guid id);
}
