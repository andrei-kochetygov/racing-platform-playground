using System.ComponentModel.DataAnnotations;

namespace Platform.API.Settings;

public class EmailFromSettings
{
    [Required]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    public required string Address { get; init; }
}
