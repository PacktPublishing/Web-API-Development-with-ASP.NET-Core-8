using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using MyWebApiDemo.Models;

namespace MyWebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IValidator<User> validator) : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Country = "USA",
            Email = ""
        },
        new User
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Age = 25,
            Country = "UK",
            Email = ""
        },
        new User
        {
            Id = 3,
            FirstName = "Sam",
            LastName = "Smith",
            Age = 40,
            Country = "New Zealand",
            Email = ""
        },
        new User
        {
            Id = 4,
            FirstName = "Tom",
            LastName = "Thumb",
            Age = 20,
            Country = "Australia",
            Email = ""
        }
    };

    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return Ok(Users);
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> Get(int id)
    {
        // The following code throws an exception when the id is not found. DO NOT use this code in production.
        var user = Users.First(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ValidationProblemDetails(validationResult.ToDictionary()));
        }
        user.Id = Users.Max(u => u.Id) + 1;
        Users.Add(user);
        return CreatedAtRoute("", new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Age = user.Age;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        Users.Remove(user);
        return NoContent();
    }

}
