using AnNguyen.Domain.Abstractions;

namespace AnNguyen.Handlers.Mock;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(IEnumerable<string> toAddresses, string subject, string body, IEnumerable<string>? bcc = null, IEnumerable<string>? cc = null, string? attachmentFilePath = null)
        => Task.CompletedTask;
    
}
