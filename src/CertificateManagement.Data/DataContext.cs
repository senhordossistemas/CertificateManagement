using CertificateManagement.Domain.Models.CertificateAggregate.Entities;
using CertificateManagement.Domain.Models.EventAggregate.Entities;
using CertificateManagement.Domain.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);

            entity.HasMany(u => u.Certificates)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(300);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();

            entity.HasMany(e => e.Certificates)
                .WithOne()
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.ToTable("Certificates");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(c => c.GeneratedDate).IsRequired();
        });

        modelBuilder.Entity<EventCertificate>(entity =>
        {
            entity.ToTable("EventCertificates");
            entity.HasKey(ec => ec.Id);
        });

        modelBuilder.Entity<UserCertificate>(entity =>
        {
            entity.ToTable("UserCertificates");
            entity.HasKey(uc => uc.Id);
        });
    }
}