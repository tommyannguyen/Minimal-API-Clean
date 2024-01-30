using AnNguyen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AnNguyen.UnitTests.Shared;

public static class InMemoryDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                     .Options;

        return new ApplicationDbContext(_contextOptions);
    }
}