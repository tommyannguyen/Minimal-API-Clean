using FastEndpoints;

namespace AnNguyen.Handlers.Abstractions;

public interface IQuery<out TResponse> : ICommand<TResponse>
{
}