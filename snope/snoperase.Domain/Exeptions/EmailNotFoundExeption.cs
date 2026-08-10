namespace snoperase.Domain.Exeptions;

public class EmailNotFoundExeption : Exception
{
    public EmailNotFoundExeption() : base("Email not found")
    {
    }
}