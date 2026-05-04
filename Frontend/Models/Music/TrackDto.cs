namespace Frontend.Models;

public class TrackDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? AlbumId { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Language { get; set; } = "";
    public int? BeatsPerMinute { get; set; }
    public TimeSpan? Length { get; set; }
    public string ExternalLink { get; set; } = "";
}