namespace snoperase.Domain.Entites;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User()
    {
        Username = Email = Password = string.Empty;
    }

    public User(Guid id, string username, string email, string password)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
    }
}