using AnNguyen.Domain;
using AnNguyen.Domain.Abstractions;
using AnNguyen.Handlers.Abstractions;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnNguyen.Handlers.Clients.Commands;

public static class UpdateClient
{
    public sealed class Command : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public sealed class MyValidator : Validator<Command>
    {
        public MyValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is required!");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("FirstName is required!");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .NotNull()
               .WithMessage("LastName is required!");
        }
    }

    public sealed class Handler : Abstractions.CommandHandler<Command>
    {
        private readonly IEmailService _emailSenderService;
        private readonly IDocumentService _documentService;

        public Handler(
            IHandlerContext context,
            IEmailService emailSenderService,
            IDocumentService documentService) : base(context)
        {
            _emailSenderService = emailSenderService;
            _documentService = documentService;
        }

        public override async Task<IHandlerResponse> ExecuteAsync(Command command, CancellationToken ct)
        {
            var client = await DbContext
                   .Clients
                   .Where(x => x.Id == command.Id)
                   .SingleAsync(ct);

            var isEmailChanged = !client.Email.Equals(command.Email);
            client.Update(command.Email, command.FirstName, command.LastName);

            await DbContext.SaveChangesAsync(ct);

            if (isEmailChanged)
                await Notification(client);

            return Success();
        }

        async Task Notification(Client client)
        {
            await SendEmailNotification(client);
        }
        Task SendEmailNotification(Client client)
            => _emailSenderService.SendEmailAsync(new List<string>() { client.Email }, "new", "new ");
        Task SyncDocuments(Client client)
            => _documentService.SyncDocuments(client);
    }
}