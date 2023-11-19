using System.Text;
using System.Text.Json;

using HttpClientDemo.Models;

using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PostsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var httpClient = _httpClientFactory.CreateClient("JsonPlaceholder");

        // Use a httpRequestMessage object
        //var httpRequestMessage = new HttpRequestMessage
        //{
        //    Method = HttpMethod.Get,
        //    RequestUri = new Uri("https://jsonplaceholder.typicode.com/posts")
        //};
        //var response = await httpClient.SendAsync(httpRequestMessage);
        //response.EnsureSuccessStatusCode();
        //var content = await response.Content.ReadAsStringAsync();
        //var posts = JsonSerializerHelper.DeserializeWithCamelCase<List<Post>>(content);

        // Use the GetStringAsync method
        var content = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
        var posts = JsonSerializerHelper.DeserializeWithCamelCase<List<Post>>(content);
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"posts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var post = JsonSerializer.Deserialize<Post>(content);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Post post)
    {
        var httpClient = _httpClientFactory.CreateClient("JsonPlaceholder");
        var json = JsonSerializer.Serialize(post);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("posts", data);
        var content = await response.Content.ReadAsStringAsync();
        var newPost = JsonSerializer.Deserialize<Post>(content);
        return Ok(newPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Post post)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var json = JsonSerializer.Serialize(post);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"posts/{id}", data);
        var content = await response.Content.ReadAsStringAsync();
        var updatedPost = JsonSerializer.Deserialize<Post>(content);
        return Ok(updatedPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.DeleteAsync($"posts/{id}");
        response.EnsureSuccessStatusCode();
        return NoContent();
    }
}
