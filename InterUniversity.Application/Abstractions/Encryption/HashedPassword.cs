namespace InterUniversity.Application.Abstractions.Encryption;

public class HashedPassword
{
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}

