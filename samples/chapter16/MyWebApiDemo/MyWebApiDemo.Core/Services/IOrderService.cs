using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Core.Services;
public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync(int userId);
    Task<List<Order>> GetOrdersAsync(int userId, OrderStatus status);
    Task<Order?> GetOrderAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}
