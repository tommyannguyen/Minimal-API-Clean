using AnNguyen.Domain;
using AnNguyen.Domain.Abstractions;

namespace AnNguyen.Handlers.Mock;

public class DocumentService : IDocumentService
{
    public Task SyncDocuments(Client client)
    {
        return Task.CompletedTask;
    }
}
