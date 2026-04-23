using System.ComponentModel.DataAnnotations;

namespace Platform.API.Settings;

public class SmtpSettings
{
    [Required]
    public required string Host { get; init; }

    [Required]
    [Range(1, 65535)]
    public required ushort Port { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
