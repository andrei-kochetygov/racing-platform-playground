using FastEndpoints;
using FluentValidation;
using SixLabors.ImageSharp;

namespace Platform.API.Endpoints.Users.UploadUserAvatar;


public sealed class UploadUserAvatarRequestValidator : Validator<UploadUserAvatarRequest>
{
    public UploadUserAvatarRequestValidator()
    {
        RuleFor(x => x.Avatar)
            .NotEmpty().WithMessage("Avatar is required.")
            .MustAsync(BeValidImage).WithMessage("Only image files are allowed.")
            .Must(file => file.Length <= 5 * 1024 * 1024).WithMessage("File size must be less than 5MB.");
    }

    public async Task<bool> BeValidImage(IFormFile file, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return false;

        await using var stream = file.OpenReadStream();

        try
        {
            var info = await Image.IdentifyAsync(stream, ct);
            return info != null;
        }
        catch
        {
            return false;
        }
    }
}
