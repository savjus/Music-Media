public class UserProfileService
{
    private readonly List<UserProfile> _profiles = new();
    private readonly List<Album> _albums = new();
    private readonly List<Track> _tracks = new();
    private readonly List<Tour> _tours = new();

    public UserProfileService()
    {
        // Seed demo data for a single example user with Id = 1
        _profiles.Add(new UserProfile
        {
            UserId = 1,
            DisplayName = "Demo Artist",
            Bio = "Example musician profile used for demo purposes.",
            DefaultLanguage = "English",
            Genres = new List<string> { "Pop", "Rock" },
            SpotifyUrl = "https://open.spotify.com/",
            YouTubeUrl = "https://www.youtube.com/"
        });

        _albums.AddRange(new[]
        {
            new Album
            {
                Id = 1,
                UserId = 1,
                Title = "First Steps",
                Year = 2015,
                Genre = "Pop",
                Description = "Debut studio album.",
                ExternalLink = "https://example.com/first-steps"
            },
            new Album
            {
                Id = 2,
                UserId = 1,
                Title = "On The Road",
                Year = 2019,
                Genre = "Rock",
                Description = "Live recordings from the first tour.",
                ExternalLink = "https://example.com/on-the-road"
            }
        });

        _tracks.AddRange(new[]
        {
            new Track
            {
                Id = 1,
                UserId = 1,
                AlbumId = 1,
                Title = "Morning Lights",
                Genre = "Pop",
                Language = "English",
                Length = TimeSpan.FromMinutes(3.5),
                ExternalLink = "https://example.com/morning-lights"
            },
            new Track
            {
                Id = 2,
                UserId = 1,
                AlbumId = 1,
                Title = "City Nights",
                Genre = "Pop",
                Language = "English",
                Length = TimeSpan.FromMinutes(4),
                ExternalLink = "https://example.com/city-nights"
            },
            new Track
            {
                Id = 3,
                UserId = 1,
                AlbumId = 2,
                Title = "Miles Ahead",
                Genre = "Rock",
                Language = "English",
                Length = TimeSpan.FromMinutes(5),
                ExternalLink = "https://example.com/miles-ahead"
            }
        });

        _tours.AddRange(new[]
        {
            new Tour
            {
                Id = 1,
                UserId = 1,
                Name = "Spring Lights Tour",
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow.AddMonths(-5),
                Location = "Baltic States"
            },
            new Tour
            {
                Id = 2,
                UserId = 1,
                Name = "Summer Festivals",
                StartDate = DateTime.UtcNow.AddMonths(1),
                EndDate = DateTime.UtcNow.AddMonths(2),
                Location = "Europe"
            }
        });
    }

    public UserProfile? GetProfile(int userId) =>
        _profiles.FirstOrDefault(p => p.UserId == userId);

    public List<Album> GetAlbumsForUser(int userId) =>
        _albums.Where(a => a.UserId == userId).ToList();

    public List<Track> GetTracksForUser(int userId) =>
        _tracks.Where(t => t.UserId == userId).ToList();

    public List<Tour> GetToursForUser(int userId) =>
        _tours.Where(t => t.UserId == userId).ToList();
}

