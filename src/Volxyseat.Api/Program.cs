using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.ClientModel;
using Volxyseat.Domain.Services;
using Volxyseat.Infrastructure.Data;
using Volxyseat.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Homologation");
builder.Services.AddDbContext<ApplicationDataContext>(opts =>
    opts.UseSqlite(connectionString));

builder.Services.AddScoped<IRepository<Client, Guid>, ClientRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ClientService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "VOLXYSEAT-API", Version = "v1" });
    });

    builder.Services.AddSwaggerGen();

    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
