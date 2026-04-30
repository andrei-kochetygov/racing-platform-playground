using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using Platform.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Platform.API.Endpoints.Identity.Register;

public class RegisterEndpoint(IServiceProvider sp) : Endpoint<RegisterRequest, Results<Ok, ValidationProblem>>
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();

    private const string confirmEmailEndpointName = "MapIdentityApi-/confirmEmail";

    public override void Configure()
    {
        Post("register");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent));
        Group<IdentityEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok, ValidationProblem>> ExecuteAsync(
        RegisterRequest registration, CancellationToken ct)
    {
        await Task.CompletedTask;

        var userManager = sp.GetRequiredService<UserManager<User>>();

        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(RegisterEndpoint)} requires a user store with email support.");
        }

        var userStore = sp.GetRequiredService<IUserStore<User>>();
        var emailStore = (IUserEmailStore<User>)userStore;
        var email = registration.Email;

        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
        {
            return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)), ct);
        }

        var user = new User();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, registration.Password);

        if (!result.Succeeded)
        {
            return CreateValidationProblem(result, ct);
        }

        await SendConfirmationEmailAsync(user, userManager, HttpContext, email);

        return TypedResults.Ok();
    }

    private async Task SendConfirmationEmailAsync(User user, UserManager<User> userManager, HttpContext context, string email)
    {
        var emailSender = sp.GetRequiredService<IEmailSender<User>>();
        var linkGenerator = sp.GetRequiredService<LinkGenerator>();
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var userId = await userManager.GetUserIdAsync(user);
        var routeValues = new RouteValueDictionary()
        {
            ["userId"] = userId,
            ["code"] = code,
        };

        var confirmEmailUrl = linkGenerator.GetUriByName(context, confirmEmailEndpointName, routeValues)
            ?? throw new NotSupportedException($"Could not find endpoint named '{confirmEmailEndpointName}'.");

        await emailSender.SendConfirmationLinkAsync(user, email, HtmlEncoder.Default.Encode(confirmEmailUrl));
    }

    private static ValidationProblem CreateValidationProblem(IdentityResult result, CancellationToken ct)
    {
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }
}
