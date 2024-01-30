namespace AnNguyen.Handlers.Clients.Queries.Models;

public sealed class ClientListModel
{
    public string Name { get; private set; }
    public string Status { get; private set; }

    public ClientListModel(Domain.Client order)
    {
        Name = order.Email;
        Status = order.FirstName;
    }

    public static ClientListModel Create(Domain.Client order)
    {
        return new ClientListModel(order);
    }
}