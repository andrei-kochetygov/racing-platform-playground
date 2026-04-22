using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Simulators.UpdateSimulator;

public class UpdateSimulatorEndpoint(AppDbContext db) : Endpoint<UpdateSimulatorRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound));
        Group<SimulatorEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateSimulatorRequest request, CancellationToken ct)
    {
        var entitiesUpdated = await db.Simulators
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Number, request.Number)
                .SetProperty(x => x.ModelId, request.ModelId), ct);

        if (entitiesUpdated == 0)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}