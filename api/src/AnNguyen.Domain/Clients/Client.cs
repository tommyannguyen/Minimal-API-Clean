using Ardalis.GuardClauses;
using AnNguyen.Domain.Abstractions;

namespace AnNguyen.Domain;

public class Client : Entity, IAggregateRoot
{
    public string Email { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    public string SearchName { get; private set; } = null!; 

    private Client()
    {
    }

    public Client(string email, string firstName, string lastName)
    {
        Guard.Against.NullOrWhiteSpace(email);
        Guard.Against.NullOrWhiteSpace(firstName);
        Guard.Against.NullOrWhiteSpace(lastName);

        Email = email;
        FirstName = firstName;
        LastName = lastName;
        SearchName= $"{FirstName.ToLower()} {LastName.ToLower()}";
    }

    public void Update(string email, string firstName, string lastName)
    {
        Guard.Against.NullOrWhiteSpace(email);
        Guard.Against.NullOrWhiteSpace(firstName);
        Guard.Against.NullOrWhiteSpace(lastName);

        Email = email;
        FirstName = firstName;
        LastName = lastName;
        SearchName = $"{FirstName.ToLower()} {LastName.ToLower()}";
    }
    public static Client Create(string email, string firstName, string lastName)
        => new(email, firstName, lastName);
}