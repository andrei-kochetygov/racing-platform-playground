using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public sealed class UpdateSimulatorModelRequestValidator : Validator<UpdateSimulatorModelRequest>
{
    public UpdateSimulatorModelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(BeUniqueName).WithMessage("Name must be unique.");
    }

    public async Task<bool> BeUniqueName(UpdateSimulatorModelRequest request, string name, CancellationToken cancellationToken)
    {
        using var scope = CreateScope();
        var db = scope.Resolve<AppDbContext>();
        return !await db.SimulatorModels.AnyAsync(x => x.Id != request.Id && x.Name == name, cancellationToken);
    }
}
