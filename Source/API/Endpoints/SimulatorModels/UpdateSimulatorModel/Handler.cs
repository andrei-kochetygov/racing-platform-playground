using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public static class UpdateSimulatorModelHandler
{
    public static async Task<IResult> HandleAsync(
        AppDbContext db,
        UpdateSimulatorModelRequest request,
        IValidator<UpdateSimulatorModelCommand> validator,
        string id
    )
    {
        var validationResult = await validator.ValidateAsync(new UpdateSimulatorModelCommand
        {
            Id = id,
            Name = request.Name,
        });

        if (!validationResult.IsValid)
        {
            return Results.UnprocessableEntity(validationResult.ToDictionary());
        }

        var entitiesUpdated = await db.SimulatorModels
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Name, request.Name)
            );

        if (entitiesUpdated == 0)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}