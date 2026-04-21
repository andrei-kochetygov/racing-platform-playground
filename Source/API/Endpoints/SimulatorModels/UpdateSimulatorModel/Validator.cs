using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public sealed class UpdateSimulatorModelRequestValidator : AbstractValidator<UpdateSimulatorModelCommand>
{
    private readonly AppDbContext _db;

    public UpdateSimulatorModelRequestValidator(AppDbContext db)
    {
        _db = db;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(BeUniqueName).WithMessage("Name must be unique.");
    }

    private async Task<bool> BeUniqueName(UpdateSimulatorModelCommand command, string name, CancellationToken cancellationToken)
    {
        return !await _db.SimulatorModels.AnyAsync(x => x.Id != command.Id && x.Name == name, cancellationToken);
    }
}
