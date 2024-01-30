using AnNguyen.Domain.Abstractions;
using AnNguyen.Handlers.Abstractions;
using AnNguyen.Handlers.Mock;
using AnNguyen.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnNguyen.Handlers.Registration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiDependencies(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IHandlerContext, HandlerContext>();

        services.AddCommonDependencies(configuration);
        services.AddDomainService(configuration);

        return services;
    }

    private static IServiceCollection AddCommonDependencies(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddFastEndpoints(dicoveryOptions =>
        {
            dicoveryOptions.Assemblies = new[] { typeof(Abstractions.CommandHandler<>).Assembly };
        });

        services.AddScoped<IHandlerRequestContext, HandlerRequestContext>();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }


    private static IServiceCollection AddDomainService(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IDocumentService, DocumentService>();
        return services;
    }

}