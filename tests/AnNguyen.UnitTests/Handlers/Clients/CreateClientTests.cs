using AnNguyen.Domain;
using AnNguyen.Domain.Abstractions;
using AnNguyen.Handlers.Clients.Commands;
using AnNguyen.Handlers.Mock;
using AnNguyen.Infrastructure.Persistence;

namespace AnNguyen.Handlers.Clients;

public class CreateClientTests
{
    private static CreateClient.Handler GetHandler(ApplicationDbContext dbContext)
        => new(HandlerContextFactory.GetHandlerContext(dbContext), 
            new EmailService(), 
            new DocumentService());

    private static void SetTestData(ApplicationDbContext dc)
    {
        var clients = new List<Client>();

        for(int i = 0;i < 10; i++)
        {
            clients.Add(Client.Create($"abc{i}@com.canh", $"first name {i}", $"last name {i}"));
        }
        dc.Clients.AddRange(clients);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Create_Client_OK()
    {
        var dbContext = InMemoryDbContextFactory.Create();
        SetTestData(dbContext);

        var command = new CreateClient.Command()
        {
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
    public async Task Create_Client_Validator_Invalid()
    {
        var dc = InMemoryDbContextFactory.Create();
        SetTestData(dc);

        var command = new CreateClient.Command()
        {
            Email = ""
        };

        await Assert.ThrowsAsync<ArgumentException>(async () => await GetHandler(dc).ExecuteAsync(command, default));
    }
}