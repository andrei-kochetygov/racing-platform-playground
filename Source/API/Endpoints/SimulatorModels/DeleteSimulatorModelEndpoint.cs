using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels;

public class DeleteSimulatorModelEndpoint(AppDbContext db) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/{id}");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent));
        Group<SimulatorModelEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var inUse = await db.Simulators.AnyAsync(x => x.ModelId == id, ct);

        if (inUse)
            ThrowError("Cannot delete simulator model because it is in use by one or more simulators.");

        await db.SimulatorModels.Where(x => x.Id == id).ExecuteDeleteAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
