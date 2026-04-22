using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels;

public class GetAllSimulatorModelsEndpoint(AppDbContext db) : EndpointWithoutRequest<IReadOnlyList<SimulatorModelResource>>
{
    public override void Configure()
    {
        Get("/");
        Description(d => d
            .Produces<IReadOnlyList<SimulatorModelResource>>(StatusCodes.Status200OK));
        Group<SimulatorModelEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var simulatorModels = await db.SimulatorModels
            .Select(x => new SimulatorModelResource {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync(ct);

        await Send.OkAsync(simulatorModels, ct);
    }
}
