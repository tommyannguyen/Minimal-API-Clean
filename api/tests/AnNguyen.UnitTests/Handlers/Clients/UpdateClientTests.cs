using AnNguyen.Domain;
using AnNguyen.Handlers.Clients.Commands;
using AnNguyen.Handlers.Mock;
using AnNguyen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnNguyen.Handlers.Clients;

public class UpdateClientTests
{
    private static UpdateClient.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new UpdateClient.Handler(
            HandlerContextFactory.GetHandlerContext(dbContext),
            new EmailService(),
            new DocumentService());
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var Clients = new List<Client> {
            Client.Create("test1@gmail.com", "firstName 1", "lastName 1" ),
            Client.Create("test2@gmail.com", "firstName 2", "lastName 2" ),
        };

        dc.Clients.AddRange(Clients);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Create_Client_OK()
    {
        var dbContext = InMemoryDbContextFactory.Create();
        SetTestData(dbContext);
        var firtClient = await dbContext.Clients.FirstAsync();

        var command = new UpdateClient.Command()
        {
            Id = firtClient.Id,
            Email = "email.com.canh",
            FirstName = "First name",
            LastName = "Last name",
        };

        var expectedEmail = command.Email;
        var expectedFirstName = command.FirstName;
        await GetHandler(dbContext).ExecuteAsync(command, default);

        var result = dbContext.Clients.SingleOrDefault(o => o.Email == expectedEmail);
        var actualCount = dbContext.Clients.Count();

        Assert.NotNull(result);
        Assert.Equal(expectedEmail, result.Email);
        Assert.Equal(expectedFirstName, result.FirstName);
    }

    [Fact]
    public async Task Create_Client_Validator()
    {
        var dbContext = InMemoryDbContextFactory.Create();
        SetTestData(dbContext);
        var firtClient = await dbContext.Clients.FirstAsync();

        var command = new UpdateClient.Command()
        {
            Id = firtClient.Id,
            Email = "",
            FirstName = "First name",
            LastName = "Last name",
        };

        await Assert.ThrowsAsync<ArgumentException>(async () => await GetHandler(dbContext).ExecuteAsync(command, default));
    }
}