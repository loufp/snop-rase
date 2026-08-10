using snoperase.Domain.ValueOdject;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TestSnope.Domain.ValueObject;

public class PasswordHashTest
{
    private static string ValidHash => "$2a$12$" + new string('x', 53);

    [Fact]
    public void Create_ValidPasswordHash()
    {
        var fakehashe = PasswordHash.Create(ValidHash);
        Assert.Equals(ValidHash, fakehashe.Value);
    }

    [Fact]
    public void Create_InvalidPasswordHash()
    {
        Assert.Throws<ArgumentException>(() => PasswordHash.Create("$2a$12$short")); 
    }
}