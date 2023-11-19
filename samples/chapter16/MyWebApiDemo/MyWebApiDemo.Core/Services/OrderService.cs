using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Core.Services;
public class OrderService : IOrderService
{
    // Use a static list as a data store for simplicity.
    private static readonly List<Order> Orders = new();

    public Task<List<Order>> GetOrdersAsync(int userId)
    {
        return Task.FromResult(Orders.Where(o => o.CustomerId == userId).ToList());
    }

    public Task<List<Order>> GetOrdersAsync(int userId, OrderStatus status)
    {
        return Task.FromResult(Orders.Where(o => o.CustomerId == userId && o.Status == status).ToList());
    }

    public Task<Order?> GetOrderAsync(int id)
    {
        return Task.FromResult(Orders.FirstOrDefault(o => o.Id == id));
    }

    public Task<Order> CreateOrderAsync(Order order)
    {
        order.Id = Orders.Count + 1;
        order.OrderNumber = $"ORD-{order.Id}";
        order.OrderDate = DateTimeOffset.UtcNow;
        order.Status = OrderStatus.Draft;

        Orders.Add(order);

        return Task.FromResult(order);
    }

    public Task<Order> UpdateOrderAsync(Order order)
    {
        var existingOrder = Orders.FirstOrDefault(o => o.Id == order.Id) ??
                            throw new InvalidOperationException($"Order with id {order.Id} not found.");
        existingOrder.ContactName = order.ContactName;
        existingOrder.Description = order.Description;
        existingOrder.Amount = order.Amount;
        existingOrder.DueDate = order.DueDate;
        existingOrder.Status = order.Status;
        existingOrder.OrderItems = order.OrderItems;

        return Task.FromResult(existingOrder);
    }

    public Task DeleteOrderAsync(int id)
    {
        var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
        if (existingOrder == null)
        {
            throw new InvalidOperationException($"Order with id {id} not found.");
        }

        Orders.Remove(existingOrder);

        return Task.CompletedTask;
    }
}
