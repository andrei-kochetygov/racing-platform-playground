namespace Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;

public sealed record CreateSimulatorModelRequest
{
    public required string Name { get; init; }
}
