

using Microsoft.AspNetCore.Authorization;

namespace Platform.API;

public static class UpdateAccessPolicy
{
    public static AuthorizationOptions AddUpdateAccessPolicy(this AuthorizationOptions builder)
    {
        builder.AddPolicy(
            nameof(UpdateAccessPolicy),
            policy => policy.AddRequirements(new UpdateAccessRequirement())
        );

        return builder;
    }
}
