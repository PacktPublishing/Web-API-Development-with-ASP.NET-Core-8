using Microsoft.AspNetCore.Mvc;

using MyWebApiDemo.Core.Models;
using MyWebApiDemo.Core.Services;

using CustomerService = MyWebApiDemo.Services.CustomerService;
using ProductService = MyWebApiDemo.Services.ProductService;

namespace MyWebApiDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly CustomerService _customerService;
    private readonly ProductService _productService;
    private readonly IOrderService _orderService;

    public OrdersController(CustomerService customerService, ProductService productService, IOrderService orderService)
    {
        _customerService = customerService;
        _productService = productService;
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Order>> Get(int id)
    {
        var order = await _orderService.GetOrderAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Order>> Post(Order order)
    {
        try
        {
            // Check if the user exists. If not, return a bad request.
            var customer = await _customerService.GetCustomerAsync(order.CustomerId);
            if (customer == null)
            {
                return BadRequest("User does not exist.");
            }

            // Check the order items in parallel. If any of them does not exist or does not have enough stock, return a bad request.
            var productCheckTasks = order.OrderItems.Select(async item =>
            {
                var product = await _productService.GetProductAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with id {item.ProductId} does not exist.");
                }

                if (product.Inventory < item.Quantity)
                {
                    throw new InvalidOperationException($"Product with id {item.ProductId} does not have enough stock.");
                }
                // The code to deduct the inventory is not implemented in this demo. DO NOT use this code in production.
            });

            await Task.WhenAll(productCheckTasks);

            var newOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
