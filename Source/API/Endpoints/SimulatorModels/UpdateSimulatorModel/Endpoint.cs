using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public class UpdateSimulatorModelEndpoint(AppDbContext db) : Endpoint<UpdateSimulatorModelRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound));
        Group<SimulatorModelEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateSimulatorModelRequest request, CancellationToken ct)
    {
        var entitiesUpdated = await db.SimulatorModels
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Name, request.Name), ct);

        if (entitiesUpdated == 0)
        {
            await Send.NotFoundAsync(ct);
        }

        await Send.NoContentAsync(ct);
    }
}