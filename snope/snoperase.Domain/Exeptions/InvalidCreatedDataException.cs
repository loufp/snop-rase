namespace snoperase.Domain.Exeptions;

public class InvalidCreatedDataException : Exception
{
    public InvalidCreatedDataException() : base("Invalid data")
    {
    }
}