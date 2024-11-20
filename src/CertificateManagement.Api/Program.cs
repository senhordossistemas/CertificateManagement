using CertificateManagement.Api.Services;
using CertificateManagement.Data;
using CertificateManagement.Domain.Contracts;
using CertificateManagement.Domain.Models.CertificateAggregate;
using CertificateManagement.Domain.Models.EventAggregate;
using CertificateManagement.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEmailSendingService, EmailSendingService>();

builder.Services.AddCors();

var app = builder.Build();

app.ApplyMigrations();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();