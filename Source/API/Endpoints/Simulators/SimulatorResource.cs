namespace Platform.API.Endpoints.Simulators;

public record SimulatorResource
{
    public required string Id { get; init; }

    public required byte Number { get; init; }

    public required string ModelId { get; init; }
}
