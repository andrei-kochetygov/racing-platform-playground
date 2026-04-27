namespace Platform.API.Endpoints.Users;

public record UserResource
{
    public required string Id { get; init; }

    public string? Email { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? AvatarUrl { get; init; }
}
