using AnNguyen.Handlers.Clients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AnNguyen.Api.Endpoints.Orders;

public class GetClientsEndpoint : ApiEndpoint<GetClients.Query, GetClients.Response>
{
    public override void Configure()
    {
        Get("clients.get");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] GetClients.Query query, CancellationToken ct) 
           => await SendAsync(query, ct);
}