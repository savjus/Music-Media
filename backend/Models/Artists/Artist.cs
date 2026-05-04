public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Language { get; set; } = "";
    public int ActiveFrom { get; set; }
    public int? ActiveTo { get; set; }
    public string Country { get; set; } = "";

    public int ProfileViews { get; set; }
}