using FastEndpoints;

namespace Platform.API.Endpoints.SimulatorModels;

public class SimulatorModelEndpointsGroup : Group
{
    public SimulatorModelEndpointsGroup()
    {
        Configure("simulatorModels", ep =>
        {
            ep.Description(d => d
                .RequireAuthorization()
                .WithTags("Simulator Models"));
        });
    }
}
