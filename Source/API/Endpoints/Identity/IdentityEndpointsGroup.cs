using FastEndpoints;

namespace Platform.API.Endpoints.Identity;

public class IdentityEndpointsGroup : Group
{
    public IdentityEndpointsGroup()
    {
        Configure("identity", ep =>
        {
            ep.Description(x => x
                .WithTags("IdentityOverride"));
        });
    }
}
