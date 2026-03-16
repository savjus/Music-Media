public class UserProfile
{
    public int UserId { get; set; }
    public string DisplayName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string DefaultLanguage { get; set; } = "";
    public List<string> Genres { get; set; } = new();
    public string SpotifyUrl { get; set; } = "";
    public string YouTubeUrl { get; set; } = "";
}

public class Album
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public string Genre { get; set; } = "";
    public string Description { get; set; } = "";
    public string ExternalLink { get; set; } = "";
}

public class Track
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? AlbumId { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Language { get; set; } = "";
    public TimeSpan? Length { get; set; }
    public string ExternalLink { get; set; } = "";
}

public class Tour
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = "";

    public bool IsPast => EndDate < DateTime.UtcNow.Date;
}

