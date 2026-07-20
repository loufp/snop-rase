namespace snoperase.Domain.ValueOdject;

public record PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 60 || !value.StartsWith("$2"))
            throw new ArgumentException("Invalid password hash format.", nameof(value));
        Value = value;
    }
    
    public static PasswordHash Create(string hash) => new(hash);
}