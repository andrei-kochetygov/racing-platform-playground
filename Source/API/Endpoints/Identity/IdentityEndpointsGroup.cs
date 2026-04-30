using FastEndpoints;

namespace Platform.API.Endpoints.Identity;

public class IdentityEndpointsGroup : Group
{
    public IdentityEndpointsGroup()
    {
        Configure("", ep =>
        {
            ep.Description(x => x
                .WithOrder(-1)
                .WithTags("Identity"));
        });
    }
}
