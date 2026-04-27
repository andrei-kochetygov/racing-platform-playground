
namespace Platform.API.Models;

public class MediaFile
{
    public required string Id { get; set; }

    public string? FileName { get; set; }

    public required string ContentType { get; set; }

    public required long Size { get; set; }

    public required string Storage { get; set; }

    public required string StorageKey { get; set; }

    public required string Status { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
