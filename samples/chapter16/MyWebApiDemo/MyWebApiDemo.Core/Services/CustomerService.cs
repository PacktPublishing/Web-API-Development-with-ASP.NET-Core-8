using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Core.Services;
public class CustomerService : ICustomerService
{
    // Use a static list as a data store for simplicity.
    private static readonly List<Customer> Customers = new()
    {
        new Customer
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Email = "john.doe@example.com",
            Country = "USA",
            PhoneNumber = "1234567890"
        },
        new Customer
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Age = 25,
            Email = "jane.doe@example.com",
            Country = "UK",
            PhoneNumber = "1234567890"
        },
        new Customer
        {
            Id = 3,
            FirstName = "Sam",
            LastName = "Smith",
            Age = 40,
            Email = "sam.smith@example.com",
            Country = "New Zealand",
            PhoneNumber = "1234567890"
        },
        new Customer
        {
            Id = 4,
            FirstName = "Tom",
            LastName = "Thumb",
            Age = 20,
            Email = "tom.thumb@example.com",
            Country = "Australia",
            PhoneNumber = "1234567890"
        }
    };

    public Task<List<Customer>> GetCustomerAsync()
    {
        return Task.FromResult(Customers);
    }

    public Task<Customer?> GetCustomerAsync(int id)
    {
        return Task.FromResult(Customers.FirstOrDefault(u => u.Id == id));
    }

    public Task<Customer> CreateCustomerAsync(Customer customer)
    {
        customer.Id = Customers.Count + 1;
        Customers.Add(customer);

        return Task.FromResult(customer);
    }

    public Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        var existingCustomer = Customers.FirstOrDefault(u => u.Id == customer.Id) ??
                               throw new ArgumentException($"The customer with Id {customer.Id} does not exist.");
        existingCustomer.FirstName = customer.FirstName;
        existingCustomer.LastName = customer.LastName;
        existingCustomer.Age = customer.Age;
        existingCustomer.Email = customer.Email;
        existingCustomer.Country = customer.Country;
        existingCustomer.PhoneNumber = customer.PhoneNumber;

        return Task.FromResult(customer);
    }

    public Task DeleteCustomerAsync(int id)
    {
        var existingCustomer = Customers.FirstOrDefault(u => u.Id == id) ??
                               throw new ArgumentException($"The customer with Id {id} does not exist.");
        Customers.Remove(existingCustomer);

        return Task.CompletedTask;
    }
}
