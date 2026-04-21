using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;

public sealed class CreateSimulatorModelRequestValidator : AbstractValidator<CreateSimulatorModelRequest>
{
    private readonly AppDbContext _db;

    public CreateSimulatorModelRequestValidator(AppDbContext db)
    {
        _db = db;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(BeUniqueName).WithMessage("Name must be unique.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _db.SimulatorModels.AnyAsync(x => x.Name == name, cancellationToken);
    }
}
