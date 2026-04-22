using FastEndpoints;
using Platform.API.OpenApi;

namespace Platform.API.Endpoints.Simulators.UpdateSimulator;

public sealed record UpdateSimulatorRequest
{
    [RouteParam]
    [OpenApiIgnore]
    public required string Id { get; init; }

    public required int Number { get; init; }

    public required string ModelId { get; init; }
}
