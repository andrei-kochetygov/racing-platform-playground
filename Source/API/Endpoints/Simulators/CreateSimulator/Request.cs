namespace Platform.API.Endpoints.Simulators.CreateSimulator;

public sealed record CreateSimulatorRequest
{
    public required int Number { get; init; }

    public required string ModelId { get; init; }
}
