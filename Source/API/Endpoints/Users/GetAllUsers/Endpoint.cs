using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Users.GetAllUsers;

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
            .Select(x => new UserResource {
                Id = x.Id,
                Email = x.Email,
            })
            .ToListAsync(ct);

        await Send.OkAsync(users, ct);
    }
}
