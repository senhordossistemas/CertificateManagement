using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.UserAggregate;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Api.Services;

public class UserService(IGenericRepository repository) : IUserService
{
    public async Task AddAsync(User user)
    {
        await repository.AddAsync(user);
        await repository.CommitAsync();
    }

    public async Task<List<User>> Get() => await repository.Query<User>().ToListAsync();

    public async Task<User> Get(int id) => await repository.Query<User>().FirstOrDefaultAsync(u => u.Id == id);
}