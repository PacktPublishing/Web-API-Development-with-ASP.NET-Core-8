using System.Text.Json;

using HttpClientDemo.Models;

using Microsoft.Net.Http.Headers;

namespace HttpClientDemo;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientDemo");
    }

    public Task<List<User>?> GetUsers()
    {
        return _httpClient.GetFromJsonAsync<List<User>>("users", _jsonSerializerOptions);
    }

    public async Task<User?> GetUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<User>($"users/{id}", _jsonSerializerOptions);
    }

    public async Task<User?> CreateUser(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("users", user, _jsonSerializerOptions);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> UpdateUser(User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"users/{user.Id}", user, _jsonSerializerOptions);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task DeleteUser(int id)
    {
        var response = await _httpClient.DeleteAsync($"users/{id}");
        response.EnsureSuccessStatusCode();
    }
}
