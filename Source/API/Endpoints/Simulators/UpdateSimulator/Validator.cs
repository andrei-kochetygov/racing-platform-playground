using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Simulators.UpdateSimulator;

public sealed class UpdateSimulatorRequestValidator : Validator<UpdateSimulatorRequest>
{
    public UpdateSimulatorRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Number is required.")
            .InclusiveBetween(byte.MinValue, byte.MaxValue).WithMessage($"Number must be between {byte.MinValue} and {byte.MaxValue}.")
            .MustAsync(BeUniqueNumber).WithMessage("Number must be unique.");

        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model is required.")
            .MustAsync(ExistInDatabase).WithMessage("Model must exist in the database.");
    }

    public async Task<bool> BeUniqueNumber(UpdateSimulatorRequest request, int number, CancellationToken cancellationToken)
    {
        using var scope = CreateScope();
        var db = scope.Resolve<AppDbContext>();
        return !await db.Simulators.AnyAsync(x => x.Id != request.Id && x.Number == number, cancellationToken);
    }

    public async Task<bool> ExistInDatabase(string modelId, CancellationToken cancellationToken)
    {
        using var scope = CreateScope();
        var db = scope.Resolve<AppDbContext>();
        return await db.SimulatorModels.AnyAsync(x => x.Id == modelId, cancellationToken);
    }
}
