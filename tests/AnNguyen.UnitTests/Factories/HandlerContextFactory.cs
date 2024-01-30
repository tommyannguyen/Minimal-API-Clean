using AnNguyen.Domain.Abstractions;
using AnNguyen.Handlers.Abstractions;
using AnNguyen.Infrastructure.Persistence;

namespace AnNguyen.UnitTests.Factories;

public static class HandlerContextFactory
{
    public static IHandlerContext GetHandlerContext(ApplicationDbContext dbContext, string? userId = null, string? email = null)
    {
        return new HandlerContext(dbContext, new HandlerRequestContext(
            userId,
            email,
            "kw"),
            LoggerFactory.Create());
    }
}