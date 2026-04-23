namespace Platform.API.Endpoints.Users;

public record UserResource
{
    public required string Id { get; init; }

    public string? Email { get; init; }
}
