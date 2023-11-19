using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7223");
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Customer>>("/api/customers");
        return result ?? new List<Customer>();
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<Customer>($"/api/customers/{id}");
        return result;
    }

    public async Task<Customer?> CreateCustomerAsync(Customer customer)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/customers", customer);
        return await result.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        var result = await _httpClient.PutAsJsonAsync($"/api/customers/{customer.Id}", customer);
        return await result.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _httpClient.DeleteAsync($"/api/customers/{id}");
    }
}
