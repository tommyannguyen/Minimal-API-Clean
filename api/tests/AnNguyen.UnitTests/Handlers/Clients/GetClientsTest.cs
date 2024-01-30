using AnNguyen.Domain;
using AnNguyen.Handlers.Clients.Queries;
using AnNguyen.Infrastructure.Persistence;

namespace AnNguyen.Handlers.Clients;

public class GetClientsTests
{
    private static GetClients.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new GetClients.Handler(HandlerContextFactory.GetHandlerContext(dbContext));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var clients = new List<Client>();

        for (int i = 0; i < 10; i++)
        {
            clients.Add(Client.Create($"abc{i}@com.canh", $"first name {i}", $"last name {i}"));
        }
        dc.Clients.AddRange(clients);
        dc.SaveChanges();
    }

    //[Fact]
    //public async Task Get_Clients_ReturnList()
    //{
    //    var dbc = InMemoryDbContextFactory.Create();
    //    SetTestData(dbc);

    //    var query = new GetClients.Query();
    //    var expected = 2;

    //    var response = await GetHandler(dbc).ExecuteAsync(query, default);

    //    Assert.Equal(expected, response.Payload.CustomerName.Count);
    //}
}