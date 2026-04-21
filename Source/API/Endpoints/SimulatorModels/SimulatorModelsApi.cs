using Platform.API.Endpoints.SimulatorModels.CreateSimulatorModel;
using Platform.API.Endpoints.SimulatorModels.UpdateSimulatorModel;

namespace Platform.API.Endpoints.SimulatorModels;

public static class SimulatorModelsApi
{
    public static IEndpointConventionBuilder MapSimulatorModelsApi(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/simulators-models");

        group.MapPost("/", CreateSimulatorModelHandler.HandleAsync)
            .Produces<SimulatorModelResource>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity);

        group.MapPut("/{id}", UpdateSimulatorModelHandler.HandleAsync)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", DeleteSimulatorModelHandler.HandleAsync)
            .Produces(StatusCodes.Status204NoContent);
            
        group.MapGet("/", GetAllSimulatorModels.HandleAsync)
            .Produces<List<SimulatorModelResource>>(StatusCodes.Status200OK);

        return group;
    }
}
