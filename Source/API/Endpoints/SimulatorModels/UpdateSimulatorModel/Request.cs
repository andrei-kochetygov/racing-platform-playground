using FastEndpoints;
using Platform.API.OpenApi;

namespace Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

public sealed record UpdateSimulatorModelRequest
{
    [RouteParam]
    [OpenApiIgnore]
    public required string Id { get; init; }

    public required string Name { get; init; }
}
