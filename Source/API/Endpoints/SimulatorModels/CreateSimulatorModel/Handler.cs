using FluentValidation;
using Platform.API.Models;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;

public static class CreateSimulatorModelHandler
{
    public static async Task<IResult> HandleAsync(
        AppDbContext db,
        CreateSimulatorModelRequest request,
        IValidator<CreateSimulatorModelRequest> validator
    )
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.UnprocessableEntity(validationResult.ToDictionary());
        }

        var simulatorModel = await db.SimulatorModels.AddAsync(new SimulatorModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name
        });

        await db.SaveChangesAsync();

        return Results.Ok(new SimulatorModelResource
        {
            Id = simulatorModel.Entity.Id,
            Name = simulatorModel.Entity.Name
        });
    }
}