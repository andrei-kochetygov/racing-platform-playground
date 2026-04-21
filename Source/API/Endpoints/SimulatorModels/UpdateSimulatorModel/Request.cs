namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public sealed record UpdateSimulatorModelRequest
{
    public required string Name { get; init; }
}
