using AnNguyen.Domain;

namespace AnNguyen.Handlers.Clients;

public class ClientsTests
{
    [Fact]
    public void CreateOrder_EmptyEmail_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Client(string.Empty, "first name", "last name"));
    }

    [Fact]
    public void CreateOrder_EmptyFirstName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Client("abc@com.canh", string.Empty, "last name"));
    }

    [Fact]
    public void CreateOrder_EmptyLastName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Client("abc@com.canh", "first name", string.Empty));
    }


}