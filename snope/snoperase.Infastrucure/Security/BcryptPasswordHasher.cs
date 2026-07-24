using snoperase.Application.Interface;

namespace snoperase.Application.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string initialPassword)
        => BCrypt.Net.BCrypt.HashPassword(initialPassword);


    public bool Verify(string plainPassword, string hash)
        => BCrypt.Net.BCrypt.Verify(plainPassword, hash);
}