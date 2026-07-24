namespace snoperase.Domain.Exeptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string email) : base($"User with this {email} already exists")
    {
    }
}