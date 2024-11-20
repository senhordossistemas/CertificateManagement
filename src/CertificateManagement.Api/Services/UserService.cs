using CertificateManagement.Data;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task AddEvent(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<List<User>> Get() => await context.Users.ToListAsync();

    public async Task<User> Get(int id) => await context.Users.FirstOrDefaultAsync(u => u.Id == id);
}