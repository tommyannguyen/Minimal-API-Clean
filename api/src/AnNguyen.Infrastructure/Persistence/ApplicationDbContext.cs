using AnNguyen.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AnNguyen.Infrastructure.Persistence;

public interface IDbContext : IDisposable
{
    public DbSet<Client> Clients { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null);
}

public class ApplicationDbContext : DbContext, IDbContext
{
    public DbSet<Client> Clients { get; set; } = null!;

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null) =>
        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
}
