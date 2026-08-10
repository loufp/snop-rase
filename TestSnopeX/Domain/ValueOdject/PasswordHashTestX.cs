using snoperase.Domain.ValueOdject;

namespace TestSnope.Domain.ValueObject;

public class PasswordHashTestX
{
    private static string ValidHash => "$2a$12$" + new string('x', 53);

    [Fact]
    public void Create_ValidPasswordHash()
    {
        var fakeHash = PasswordHash.Create(ValidHash);
        Assert.Equal(fakeHash.Value, ValidHash);
    }

    [Fact]
    public void Create_InvalidPasswordHash()
    {
        Assert.Throws<ArgumentException>(() => PasswordHash.Create("$2a$12$short"));
    }
}