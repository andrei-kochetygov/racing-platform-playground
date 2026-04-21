namespace Platform.API.Endpoints.Users;

public static class UsersApi
{
    public static IEndpointConventionBuilder MapUsersApi(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/users");

        group.MapGet("/", GetAllUsersHandler.HandleAsync)
            .Produces<List<UserResource>>(StatusCodes.Status200OK);

        return group;
    }
}
