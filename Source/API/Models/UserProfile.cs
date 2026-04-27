namespace Platform.API.Models;

public class UserProfile
{
    public required string Id { get; set; }

    public required string UserId { get; set; }

    public User User { get; set; } = null!;

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? AvatarId { get; set; }

    public MediaFile? Avatar { get; set; }
}
