using AnNguyen.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnNguyen.Infrastructure.Persistence;


public static class ConnectionStrings
{
    public static string Sqlite = "Data Source=C:\\test.db";
}
public class MigrationAppDbContext : ApplicationDbContext
{
    public DbSet<Client> Clients { get; set; } = null!;

    public MigrationAppDbContext() { }
    public MigrationAppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(ConnectionStrings.Sqlite);
}

