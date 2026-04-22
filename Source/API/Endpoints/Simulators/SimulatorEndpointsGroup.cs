using FastEndpoints;

namespace Platform.API.Endpoints.Simulators;

public class SimulatorEndpointsGroup : Group
{
    public SimulatorEndpointsGroup()
    {
        Configure("simulators", ep =>
        {
            ep.Description(d => d
                .RequireAuthorization()
                .WithTags("Simulators"));
        });
    }
}
