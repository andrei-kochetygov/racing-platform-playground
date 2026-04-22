using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Simulators;

public class DeleteSimulatorEndpoint(AppDbContext db) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/{id}");
        Description(d => d
            .Produces(StatusCodes.Status204NoContent));
        Group<SimulatorEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        await db.Simulators.Where(x => x.Id == id).ExecuteDeleteAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
