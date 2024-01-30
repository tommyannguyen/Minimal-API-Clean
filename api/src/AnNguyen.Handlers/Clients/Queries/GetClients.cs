using AnNguyen.Domain;
using AnNguyen.Handlers.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace AnNguyen.Handlers.Clients.Queries;

public static class GetClients
{
    public sealed class Query : IQuery<IHandlerResponse<Response>>
    {
        public string SearchName { get; set; } = "";
    }

    public sealed class Response
    {
        public IList<ResponseItem> Items { get; set; }

        public Response(IList<Domain.Client> clients)
        {
            Items = clients
                    .Select(client => new ResponseItem(client))
                     .ToList();
        }

        public static Response Create(IList<Domain.Client> clients)
            => new(clients);
    }

    public sealed class ResponseItem
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public ResponseItem(Domain.Client client)
        {
            Id = client.Id;
            Email = client.Email;
            FirstName = client.FirstName;
            LastName = client.LastName;
        }
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Query query, CancellationToken ct)
        {
            var searchName = query.SearchName.ToLower();
            var clients = await DbContext
                   .Clients
                   .Where(x => x.SearchName.Contains(searchName))
                   .ToListAsync(ct);

            if (clients is null)
            {
                return Error("Clients with search name {0} not found.", query.SearchName);
            }

            return Success(new Response(clients));
        }
    }
}