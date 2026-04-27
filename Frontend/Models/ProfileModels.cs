public class UserProfileDto
{
    public int UserId { get; set; }
    public string DisplayName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string DefaultLanguage { get; set; } = "";
    public List<string> Genres { get; set; } = new();
    public string SpotifyUrl { get; set; } = "";
    public string YouTubeUrl { get; set; } = "";
}

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

public class TourDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = "";
    public bool IsPast { get; set; }
}

public class CommentDto
{
    public int Id { get; set; }
    public int ProfileUserId { get; set; }
    public int AuthorUserId { get; set; }
    public string AuthorName { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
}

public class ProfileResponseDto
{
    public UserProfileDto Profile { get; set; } = new();
    public List<AlbumDto> Albums { get; set; } = new();
    public List<TrackDto> Tracks { get; set; } = new();
    public List<TourDto> Tours { get; set; } = new();
    public List<CommentDto> Comments { get; set; } = new();
}

