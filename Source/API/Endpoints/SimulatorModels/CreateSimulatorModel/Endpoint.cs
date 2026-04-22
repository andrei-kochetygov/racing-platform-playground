using FastEndpoints;
using Platform.API.Models;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;

public class CreateSimulatorModelEndpoint(AppDbContext db) : Endpoint<CreateSimulatorModelRequest, SimulatorModelResource>
{
    public override void Configure()
    {
        Post("/");
        Description(d => d
            .Produces<SimulatorModelResource>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity));
        Group<SimulatorModelEndpointsGroup>();
    }

    public override async Task HandleAsync(CreateSimulatorModelRequest request, CancellationToken ct)
    {
        var simulatorModel = await db.SimulatorModels.AddAsync(new SimulatorModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name
        }, ct);

        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new SimulatorModelResource
        {
            Id = simulatorModel.Entity.Id,
            Name = simulatorModel.Entity.Name
        }, ct);
    }
}
