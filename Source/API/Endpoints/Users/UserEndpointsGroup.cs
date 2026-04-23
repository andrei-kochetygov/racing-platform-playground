using FastEndpoints;

namespace Platform.API.Endpoints.Users;

public class UserEndpointsGroup : Group
{
    public UserEndpointsGroup()
    {
        Configure("users", ep =>
        {
            ep.Description(x => x
                .RequireAuthorization()
                .WithTags("Users"));
        });
    }
}
