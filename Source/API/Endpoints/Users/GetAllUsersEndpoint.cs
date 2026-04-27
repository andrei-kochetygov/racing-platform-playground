using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Users;

public class GetAllUsersEndpoint(AppDbContext db) : EndpointWithoutRequest<IReadOnlyList<UserResource>>
{
    public override void Configure()
    {
        Get("/");
        Description(d => d
            .Produces<IReadOnlyList<UserResource>>(StatusCodes.Status200OK));
        Group<UserEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await db.Users
            .Include(x => x.Profile)
            .Select(x => new UserResource
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.Profile != null ? x.Profile.FirstName : null,
                LastName = x.Profile != null ? x.Profile.LastName : null,
                AvatarUrl = x.Profile != null && x.Profile.Avatar != null
                    ? $"/uploads/{x.Profile.Avatar.StorageKey}"
                    : null
            })
            .ToListAsync(ct);

        await Send.OkAsync(users, ct);
    }
}
