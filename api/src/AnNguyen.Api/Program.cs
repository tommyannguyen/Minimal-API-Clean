using AnNguyen.Handlers.Registration;
using AnNguyen.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .WriteTo.Console();
});

builder.Services.AddResponseCaching();

builder.Services.SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.DocumentName = "Initial Release";
            s.Title = "AnNguyen.Api";
            s.Version = "v1.0";
        };
    })
    .SwaggerDocument(o =>
    {
        o.MaxEndpointVersion = 1;
        o.DocumentSettings = s =>
        {
            s.DocumentName = "Release 1.0";
            s.Title = "AnNguyen.Api";
            s.Version = "v1.0";
        };
    });

builder.Services.AddApiDependencies(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseResponseCaching();
app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});

app.UseSwaggerGen();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.Run();