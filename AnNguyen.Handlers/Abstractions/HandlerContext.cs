using AnNguyen.Infrastructure.Persistence;

namespace AnNguyen.Handlers.Abstractions;

public sealed class HandlerContext : IHandlerContext
{
    public IDbContext DbContext { get; private set; }
    public IHandlerRequestContext RequestContext { get; private set; }
    public Serilog.ILogger Logger { get; private set; }

    public HandlerContext(
        IDbContext dbContext, 
        IHandlerRequestContext requestContext, 
        Serilog.ILogger logger)
    {
        DbContext = dbContext;
        RequestContext = requestContext;
        Logger = logger;
    }
}