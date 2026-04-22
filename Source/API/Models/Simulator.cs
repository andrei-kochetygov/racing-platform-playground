namespace Platform.API.Models;

public class Simulator
{
    public required string Id { get; set; }

    public required byte Number { get; set; }

    public required string ModelId { get; set; }

    public required SimulatorModel Model { get; set; }
}
