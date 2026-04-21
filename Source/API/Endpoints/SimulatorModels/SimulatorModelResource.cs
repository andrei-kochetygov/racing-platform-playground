namespace Platform.API.Endpoints.SimulatorModels;

public record SimulatorModelResource
{
    public required string Id { get; init; }

    public required string Name { get; init; }
}
