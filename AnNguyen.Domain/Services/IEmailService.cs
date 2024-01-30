namespace AnNguyen.Domain.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(IEnumerable<string> toAddresses, string subject, string body, IEnumerable<string>? bcc = null, IEnumerable<string>? cc = null, string? attachmentFilePath = null);
}
