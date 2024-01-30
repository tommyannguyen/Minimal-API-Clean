using AnNguyen.Domain;
using AnNguyen.Domain.Abstractions;
using AnNguyen.Handlers.Abstractions;
using FastEndpoints;
using FluentValidation;
using Serilog;

namespace AnNguyen.Handlers.Clients.Commands;

public static class CreateClient
{
    public sealed class Command : ICommand<IHandlerResponse<Response>>
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

    }

    public sealed class Response
    {
        public Guid Id { get; private set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public Response(Domain.Client client)
        {
            Id = client.Id;
            Email = client.Email;
            FirstName = client.FirstName;
            LastName = client.LastName;
        }

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

    public sealed class Handler : Abstractions.CommandHandler<Command, Response>
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailSenderService;
        private readonly IDocumentService _documentService;

        public Handler(
            IHandlerContext context,
            IEmailService emailSenderService,
            IDocumentService documentService) : base(context)
        {
            _logger = context.Logger;
            _emailSenderService = emailSenderService;
            _documentService = documentService;
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Command command, CancellationToken ct)
        {
            var client = Domain.Client.Create(command.Email, command.FirstName, command.LastName);

            await DbContext.Clients.AddAsync(client, ct);
            await DbContext.SaveChangesAsync(ct);

            await Notification(client);
            return Success(new Response(client));
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