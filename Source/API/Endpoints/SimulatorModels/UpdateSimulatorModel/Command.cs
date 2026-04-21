namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public sealed record UpdateSimulatorModelCommand
{
    public required string Id { get; init; }

    public required string Name { get; init; }
}
