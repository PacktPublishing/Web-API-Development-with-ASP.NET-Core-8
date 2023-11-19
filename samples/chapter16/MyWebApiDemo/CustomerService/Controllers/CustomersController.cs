using Microsoft.AspNetCore.Mvc;

using MyWebApiDemo.Core.Models;
using MyWebApiDemo.Core.Services;

namespace CustomerService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customers = await _customerService.GetCustomerAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Customer>> Get(int id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Customer>> Post(Customer customer)
    {
        try
        {
            var newCustomer = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        if (await _customerService.GetCustomerAsync(id) == null)
        {
            return NotFound();
        }

        try
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
            return Ok(updatedCustomer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
