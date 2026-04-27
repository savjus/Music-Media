namespace Frontend.Models;

public class AlbumDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public string Genre { get; set; } = "";
    public string Description { get; set; } = "";
    public string ExternalLink { get; set; } = "";
}