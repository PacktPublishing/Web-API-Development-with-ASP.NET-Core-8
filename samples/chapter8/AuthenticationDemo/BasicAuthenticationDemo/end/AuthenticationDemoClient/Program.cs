// See https://aka.ms/new-console-template for more information

using AuthenticationDemoClient;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var userName = string.Empty;
while (userName == null || string.IsNullOrWhiteSpace(userName))
{
    Console.WriteLine("Please type your username:");
    userName = Console.ReadLine();
}

var password = string.Empty;
while (password == null || string.IsNullOrWhiteSpace(password))
{
    Console.WriteLine("Please type your password:");
    password = Console.ReadLine();
}

var httpClient = new HttpClient();
// Create a post request with the user name and password
var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5056/authentication/login");
request.Content = new StringContent(JsonSerializer.Serialize(new LoginModel()
{
    UserName = userName,
    Password = password
}), Encoding.UTF8, "application/json");
// Send the request to the server
var response = await httpClient.SendAsync(request);
var token = string.Empty;
// If the request is successful, read the response
if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();
    // Deserialize the response to get the token
    var jwtToken = JsonSerializer.Deserialize<JwtToken>(content);
    Console.WriteLine(jwtToken.token);
    token = jwtToken.token;
}

// Create a new request to get the protected data
request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5056/WeatherForecast");
// Add the token to the request header
request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
// Send the request to the server
response = await httpClient.SendAsync(request);
// If the request is successful, read the response
if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();
    // Deserialize the response to get the token
    var weatherForecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(content);
    foreach (var weatherForecast in weatherForecasts)
    {
        Console.WriteLine("Date: {0:d}", weatherForecast.Date);
        Console.WriteLine($"Temperature (C): {weatherForecast.TemperatureC}");
        Console.WriteLine($"Temperature (F): {weatherForecast.TemperatureF}");
        Console.WriteLine($"Summary: {weatherForecast.Summary}");
    }
}
else
{
    Console.WriteLine(response.ReasonPhrase);
}

Console.ReadLine();
