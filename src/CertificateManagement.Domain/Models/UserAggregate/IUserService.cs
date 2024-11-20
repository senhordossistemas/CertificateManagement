using CertificateManagement.Domain.Models.UserAggregate.Entities;

namespace CertificateManagement.Domain.Models.UserAggregate;

public interface IUserService
{
    Task AddEvent(User user);
    Task<List<User>> Get();
    Task<User> Get(int id);
}