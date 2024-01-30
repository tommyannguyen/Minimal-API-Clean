using AnNguyen.Handlers.Clients.Commands;

namespace AnNguyen.Api.Endpoints.Clients;

public class CreateClientEndpoint : ApiEndpoint<CreateClient.Command, CreateClient.Response>
{
    public override void Configure()
    {
        Post("client.create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateClient.Command command, CancellationToken ct) 
        => await SendAsync(command, ct);
}