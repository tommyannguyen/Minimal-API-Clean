

namespace AnNguyen.Domain.Abstractions;

public interface IDocumentService
{
    Task SyncDocuments(Client client);
}