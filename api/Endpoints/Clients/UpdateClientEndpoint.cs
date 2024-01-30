using AnNguyen.Handlers.Clients.Commands;

namespace AnNguyen.Api.Endpoints.Clients;

public class UpdateClientEndpoint : ApiEndpoint<UpdateClient.Command>
{
    public override void Configure()
    {
        Post("Clients.update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClient.Command command, CancellationToken ct) => await SendAsync(command, ct);
}