using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;


public sealed class CreateSimulatorModelRequestValidator : Validator<CreateSimulatorModelRequest>
{
    public CreateSimulatorModelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(BeUniqueName).WithMessage("Name must be unique.");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        using var scope = CreateScope();
        var db = scope.Resolve<AppDbContext>();
        return !await db.SimulatorModels.AnyAsync(x => x.Name == name, cancellationToken);
    }
}
