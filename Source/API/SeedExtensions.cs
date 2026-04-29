using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Platform.API;

public static class SeedExtensions
{
    public static async Task SeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await CreateRoleAsync(
            roleManager,
            Roles.Administrator,
            [
                Permissions.SimulatorModules.Read,
                Permissions.SimulatorModules.Write,
                Permissions.Simulators.Read,
                Permissions.Simulators.Write,
                Permissions.Users.Read,
                Permissions.Users.WriteOwn
            ]);

        await CreateRoleAsync(
            roleManager,
            Roles.User,
            [
                Permissions.Users.WriteOwn
            ]);
    }

    private static async Task CreateRoleAsync(
        RoleManager<IdentityRole> roleManager,
        string roleName,
        IEnumerable<string> permissions)
    {
        var role = await roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            role = new IdentityRole(roleName);

            await roleManager.CreateAsync(role);
        }

        var existingClaims = await roleManager.GetClaimsAsync(role);

        foreach (var permission in permissions)
        {
            if (existingClaims.Any(x =>
                    x.Type == CustomClaimTypes.Permission &&
                    x.Value == permission))
            {
                continue;
            }

            await roleManager.AddClaimAsync(
                role,
                new Claim(CustomClaimTypes.Permission, permission));
        }
    }
}
