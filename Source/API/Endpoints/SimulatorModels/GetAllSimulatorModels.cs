using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels;

public static class GetAllSimulatorModels
{
    public static async Task<IResult> HandleAsync(AppDbContext db)
    {
        var simulatorModels = await db.SimulatorModels.Select(x => new SimulatorModelResource
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();

        return Results.Ok(simulatorModels);
    }
}