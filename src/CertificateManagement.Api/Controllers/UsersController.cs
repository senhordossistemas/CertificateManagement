using CertificateManagement.Domain.Models.UserAggregate;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await userService.AddAsync(user);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.Get();
        return Ok(users);
    }
}
