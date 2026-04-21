using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.API.Endpoints.SimulatorModels;
using Platform.API.Endpoints.Users;
using Platform.API.Models;
using Platform.API.Persistence;
using Scalar.AspNetCore;

namespace Platform.API;

public class Program
{
    public static void Main(string[] args)
    {
        var appBuilder = WebApplication.CreateBuilder(args);
        var services = appBuilder.Services;

        services.AddFastEndpoints();

        services.AddValidatorsFromAssemblyContaining<Program>();

        services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
        services.AddTransient<IEmailSender<User>, EmailSender>();

        services
            .AddIdentityCore<User>(options => {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        services
            .AddAuthentication(IdentityConstants.BearerScheme)
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorization();
        services.AddOpenApi(options => {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        var app = appBuilder.Build();

        app.UseFastEndpoints();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapIdentityApi<User>().WithTags("Identity");
        app.MapSimulatorModelsApi().RequireAuthorization().WithTags("Simulator Models");

        app.MapOpenApi();
        app.MapScalarApiReference(
            options => options.DisableAgent()
                .DisableTelemetry()
                .HideDeveloperTools()
                .AddPreferredSecuritySchemes("BearerAuth")
        );

        app.ApplyMigrations();

        app.Run();
    }
}
