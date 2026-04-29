using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Platform.API.Models;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Users.UploadUserAvatar;

public class UploadUserAvatarEndpoint(AppDbContext db, IWebHostEnvironment env, IAuthorizationService authorizationService) : Endpoint<UploadUserAvatarRequest>
{
    public override void Configure()
    {
        Put("{id}/avatar");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound));
        Group<UserEndpointsGroup>();
        AllowFileUploads();
    }

    public override async Task HandleAsync(UploadUserAvatarRequest request, CancellationToken ct)
    {
        var userId = Route<string>("id");

        if (userId is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var profile = await db.UserProfiles.FirstAsync(x => x.UserId == userId, ct);

        var authorization = await authorizationService.AuthorizeAsync(User, profile, nameof(UpdateAccessPolicy));

        if (!authorization.Succeeded)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var currentUserId = (User.FindFirst("sub")?.Value
            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
            ?? throw new UnauthorizedAccessException("User is not authenticated");

        if (userId != currentUserId)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var file = request.Avatar;

        var mediaId = Guid.NewGuid().ToString();
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{mediaId}{extension}";

        var uploadsPath = Path.Combine(env.WebRootPath, "uploads");
        var subFolder = DateTime.UtcNow.ToString("yyyy/MM");
        var fullDirectory = Path.Combine(uploadsPath, subFolder);
        var storageKey = Path.Combine(subFolder, fileName).Replace("\\", "/");

        var media = new MediaFile
        {
            Id = mediaId,
            FileName = Path.GetFileName(file.FileName),
            ContentType = file.ContentType,
            Size = file.Length,
            Storage = "local",
            StorageKey = storageKey,
            Status = "pending",
            CreatedAt = DateTime.UtcNow
        };

        await using var creationTransaction = await db.Database.BeginTransactionAsync(ct);

        await db.MediaFiles.Where(x => x.Id == profile.AvatarId && x.Status != "obsolete")
            .ExecuteUpdateAsync(p => p
                .SetProperty(x => x.Status, "obsolete")
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        profile.AvatarId = null;

        await db.MediaFiles.AddAsync(media, ct);

        await db.SaveChangesAsync(ct);

        await creationTransaction.CommitAsync(ct);

        Directory.CreateDirectory(fullDirectory);

        var filePath = Path.Combine(fullDirectory, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream, ct);

        await using var attachTransaction = await db.Database.BeginTransactionAsync(ct);

        await db.MediaFiles.Where(x => x.Id == mediaId)
            .ExecuteUpdateAsync(p => p
                .SetProperty(x => x.Status, "attached")
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        profile.AvatarId = mediaId;

        await db.SaveChangesAsync(ct);

        await attachTransaction.CommitAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
