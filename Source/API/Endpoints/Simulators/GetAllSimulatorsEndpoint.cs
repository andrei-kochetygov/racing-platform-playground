using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Simulators;

public class GetAllSimulatorsEndpoint(AppDbContext db) : EndpointWithoutRequest<IReadOnlyList<SimulatorResource>>
{
    public override void Configure()
    {
        Get("/");
        Description(d => d
            .Produces<IReadOnlyList<SimulatorResource>>(StatusCodes.Status200OK));
        Group<SimulatorEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var simulators = await db.Simulators
            .Select(x => new SimulatorResource
            {
                Id = x.Id,
                Number = x.Number,
                ModelId = x.ModelId,
            })
            .ToListAsync(ct);

        await Send.OkAsync(simulators, ct);
    }
}
