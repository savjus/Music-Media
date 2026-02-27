namespace Frontend.Models;

public class ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Language { get; set; } = "";
    public int ActiveFrom { get; set; }
    public int? ActiveTo { get; set; }
    public string Country { get; set; } = "";
    public string? Description { get; set; }
}