namespace Platform.API.Endpoints.Users.UploadUserAvatar;

public sealed record UploadUserAvatarRequest
{
    public required IFormFile Avatar { get; set; }
}
