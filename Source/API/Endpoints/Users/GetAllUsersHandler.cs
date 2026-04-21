using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Users;

public static class GetAllUsersHandler
{
    public static async Task<IResult> HandleAsync(AppDbContext db)
    {
        var users = await db.Users
            .Select(x => new UserResource {
                Id = x.Id,
                Email = x.Email,
            })
            .ToListAsync();

        return Results.Ok(users);
    }
}
