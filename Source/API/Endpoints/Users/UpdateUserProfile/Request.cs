namespace Platform.API.Endpoints.Users.UpdateUserProfile;

public sealed record UpdateUserProfileRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }
}
