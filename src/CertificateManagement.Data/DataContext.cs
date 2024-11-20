using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Event> Events { get; init; }
    public DbSet<Certificate> Certificates { get; init; }
    public DbSet<EventCertificate> EventCertificates { get; init; }
    public DbSet<UserCertificate> UserCertificates { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}