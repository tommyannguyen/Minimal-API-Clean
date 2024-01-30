using AnNguyen.Infrastructure.Persistence;
using Serilog;

namespace AnNguyen.Handlers.Abstractions;

public interface IHandlerContext
{
    IDbContext DbContext { get; }
    ILogger Logger { get; }
    IHandlerRequestContext RequestContext { get; }
}