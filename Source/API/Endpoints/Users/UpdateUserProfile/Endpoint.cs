using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;
using Platform.API.Models;

namespace Platform.API.Endpoints.Users.UpdateUserProfile;

public class UpdateUserProfileEndpoint(AppDbContext db) : Endpoint<UpdateUserProfileRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound));
        Group<UserEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateUserProfileRequest request, CancellationToken ct)
    {
        var userId = Route<string>("id");

        if (userId is null)
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

        var userExists = await db.Users.AnyAsync(x => x.Id == userId, ct);

        if (!userExists)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var profile = await db.UserProfiles.SingleOrDefaultAsync(x => x.UserId == userId, ct) ?? new UserProfile
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var entiresAffected = await db.UserProfiles.Where(x => x.UserId == userId).ExecuteUpdateAsync(p => p
                .SetProperty(x => x.FirstName, request.FirstName)
                .SetProperty(x => x.LastName, request.LastName), ct);

        if (entiresAffected == 0)
        {
            try
            {
                await db.UserProfiles.AddAsync(new UserProfile
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                }, ct);

                await db.SaveChangesAsync(ct);
            }
            catch (DbUpdateException)
            {
                await db.UserProfiles.Where(x => x.UserId == userId).ExecuteUpdateAsync(p => p
                    .SetProperty(x => x.FirstName, request.FirstName)
                    .SetProperty(x => x.LastName, request.LastName), ct);
            }
        }

        await Send.NoContentAsync(ct);
    }
}
