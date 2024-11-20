using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CertificateManagement.Data;

public static class DatabaseConfig
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}