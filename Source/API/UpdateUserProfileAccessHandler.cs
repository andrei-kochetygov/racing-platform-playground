using Microsoft.AspNetCore.Authorization;
using Platform.API.Models;
using System.Security.Claims;

namespace Platform.API;

public class UpdateUserProfileAccessHandler
    : AuthorizationHandler<UpdateAccessRequirement, UserProfile>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        UpdateAccessRequirement requirement,
        UserProfile profile
    )
    {
        var currentUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId is null)
            return Task.CompletedTask;

        if (currentUserId == profile.UserId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
