using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Platform.API.Persistence;

namespace Platform.API.Endpoints.SimulatorModels;

public static class DeleteSimulatorModelHandler
{
    public static async Task<IResult> HandleAsync(AppDbContext db, string id)
    {
        await db.SimulatorModels.Where(x => x.Id == id).ExecuteDeleteAsync();

        return Results.NoContent();
    }
}