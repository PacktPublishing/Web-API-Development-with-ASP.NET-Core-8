using HttpClientDemo.Models;

using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        var users = await usersService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await usersService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        var createdUser = await usersService.CreateUser(user);
        return Ok(createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Put(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        var updatedUser = await usersService.UpdateUser(user);
        return Ok(updatedUser);
    }
}
