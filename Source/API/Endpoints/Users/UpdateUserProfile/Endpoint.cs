using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;
using Platform.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Platform.API.Endpoints.Users.UpdateUserProfile;

public class UpdateUserProfileEndpoint(AppDbContext db, IAuthorizationService authorizationService) : Endpoint<UpdateUserProfileRequest>
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

        var authorization = await authorizationService.AuthorizeAsync(User, profile, nameof(UpdateAccessPolicy));

        if (!authorization.Succeeded)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

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
