using FastEndpoints;
using Platform.API.Models;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.Simulators.CreateSimulator;

public class CreateSimulatorEndpoint(AppDbContext db) : Endpoint<CreateSimulatorRequest, SimulatorResource>
{
    public override void Configure()
    {
        Post("/");
        Description(d => d
            .Produces<SimulatorResource>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity));
        Group<SimulatorEndpointsGroup>();
    }

    public override async Task HandleAsync(CreateSimulatorRequest request, CancellationToken ct)
    {
        var simulatorModel = await db.SimulatorModels.FindAsync([request.ModelId], ct);

        if (simulatorModel is null)
            ThrowError(x => x.ModelId, "Simulator model not found!");

        var simulator = await db.Simulators.AddAsync(new Simulator
        {
            Id = Guid.NewGuid().ToString(),
            Number = (byte) request.Number,
            ModelId = request.ModelId,
            Model = simulatorModel,
        }, ct);

        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new SimulatorResource
        {
            Id = simulator.Entity.Id,
            Number = simulator.Entity.Number,
            ModelId = simulator.Entity.ModelId,
        }, ct);
    }
}
