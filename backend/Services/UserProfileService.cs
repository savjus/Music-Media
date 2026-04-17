public class UserProfileService
{
    private readonly List<UserProfile> _profiles = new();
    private readonly List<Album> _albums = new();
    private readonly List<Track> _tracks = new();
    private readonly List<Tour> _tours = new();

    public UserProfileService()
    {
        // Seed demo data for a single example user with Id = 1
        _profiles.AddRange(new[]
        {
            new UserProfile
            {
                UserId = 1,
                DisplayName = "Demo Artist",
                Bio = "Example musician profile used for demo purposes.",
                DefaultLanguage = "English",
                Genres = new List<string> { "Pop", "Rock" },
                SpotifyUrl = "https://open.spotify.com/",
                YouTubeUrl = "https://www.youtube.com/"
            },
            new UserProfile
            {
                UserId = 2,
                DisplayName = "Neon Harbor",
                Bio = "Synth-pop duo blending retro textures with modern vocals.",
                DefaultLanguage = "English",
                Genres = new List<string> { "Synth-Pop", "Electronic" },
                SpotifyUrl = "https://open.spotify.com/artist/neon-harbor",
                YouTubeUrl = "https://www.youtube.com/@neonharbor"
            },
            new UserProfile
            {
                UserId = 3,
                DisplayName = "Baltic Echo",
                Bio = "Indie folk project focused on acoustic storytelling and coastal moods.",
                DefaultLanguage = "Lithuanian",
                Genres = new List<string> { "Indie", "Folk" },
                SpotifyUrl = "https://open.spotify.com/artist/baltic-echo",
                YouTubeUrl = "https://www.youtube.com/@balticecho"
            }
        }
        );

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
            },
            new Album
            {
                Id = 3,
                UserId = 2,
                Title = "Midnight Signals",
                Year = 2020,
                Genre = "Synth-Pop",
                Description = "A neon-soaked debut full of analog synth hooks.",
                ExternalLink = "https://example.com/midnight-signals"
            },
            new Album
            {
                Id = 4,
                UserId = 2,
                Title = "Afterglow City",
                Year = 2023,
                Genre = "Electronic",
                Description = "Darker electronic themes with cinematic production.",
                ExternalLink = "https://example.com/afterglow-city"
            },
            new Album
            {
                Id = 5,
                UserId = 3,
                Title = "Coastal Letters",
                Year = 2018,
                Genre = "Folk",
                Description = "Warm acoustic debut inspired by life by the sea.",
                ExternalLink = "https://example.com/coastal-letters"
            },
            new Album
            {
                Id = 6,
                UserId = 3,
                Title = "Northern Pines",
                Year = 2022,
                Genre = "Indie",
                Description = "A textured indie-folk record with intimate vocals.",
                ExternalLink = "https://example.com/northern-pines"
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
            },
            new Track
            {
                Id = 4,
                UserId = 2,
                AlbumId = 3,
                Title = "Glass Neon",
                Genre = "Synth-Pop",
                Language = "English",
                Length = TimeSpan.FromMinutes(3.8),
                ExternalLink = "https://example.com/glass-neon"
            },
            new Track
            {
                Id = 5,
                UserId = 2,
                AlbumId = 3,
                Title = "Harbor Lights",
                Genre = "Synth-Pop",
                Language = "English",
                Length = TimeSpan.FromMinutes(4.1),
                ExternalLink = "https://example.com/harbor-lights"
            },
            new Track
            {
                Id = 6,
                UserId = 2,
                AlbumId = 4,
                Title = "Soft Static",
                Genre = "Electronic",
                Language = "English",
                Length = TimeSpan.FromMinutes(4.4),
                ExternalLink = "https://example.com/soft-static"
            },
            new Track
            {
                Id = 7,
                UserId = 3,
                AlbumId = 5,
                Title = "Paper Boats",
                Genre = "Folk",
                Language = "Lithuanian",
                Length = TimeSpan.FromMinutes(3.2),
                ExternalLink = "https://example.com/paper-boats"
            },
            new Track
            {
                Id = 8,
                UserId = 3,
                AlbumId = 6,
                Title = "Amber Wind",
                Genre = "Indie",
                Language = "Lithuanian",
                Length = TimeSpan.FromMinutes(4.0),
                ExternalLink = "https://example.com/amber-wind"
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
            },
            new Tour
            {
                Id = 3,
                UserId = 2,
                Name = "Neon Nights",
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(-2),
                Location = "UK"
            },
            new Tour
            {
                Id = 4,
                UserId = 2,
                Name = "Afterglow Live",
                StartDate = DateTime.UtcNow.AddMonths(2),
                EndDate = DateTime.UtcNow.AddMonths(3),
                Location = "Scandinavia"
            },
            new Tour
            {
                Id = 5,
                UserId = 3,
                Name = "Forest Sessions",
                StartDate = DateTime.UtcNow.AddMonths(-8),
                EndDate = DateTime.UtcNow.AddMonths(-7),
                Location = "Baltic States"
            },
            new Tour
            {
                Id = 6,
                UserId = 3,
                Name = "Coastal Evenings",
                StartDate = DateTime.UtcNow.AddMonths(4),
                EndDate = DateTime.UtcNow.AddMonths(5),
                Location = "Northern Europe"
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

    public bool UpdateProfile(UserProfile updatedProfile)
    {
        var existingProfile = _profiles.FirstOrDefault(p => p.UserId == updatedProfile.UserId);
        if (existingProfile == null)
        {
            return false;
        }

        existingProfile.DisplayName = updatedProfile.DisplayName;
        existingProfile.Bio = updatedProfile.Bio;
        existingProfile.DefaultLanguage = updatedProfile.DefaultLanguage;
        existingProfile.Genres = updatedProfile.Genres;
        existingProfile.SpotifyUrl = updatedProfile.SpotifyUrl;
        existingProfile.YouTubeUrl = updatedProfile.YouTubeUrl;

        return true;
    }
    public bool AddTrack(Track track)
    {
        var userExists = _profiles.Any(p => p.UserId == track.UserId);
        if (!userExists)
        {
            return false;
        }

        track.Id = _tracks.Any() ? _tracks.Max(t => t.Id) + 1 : 1;
        _tracks.Add(track);

        return true;
    }

    public bool AddAlbum(Album album)
    {
        var userExists = _profiles.Any(p => p.UserId == album.UserId);
        if (!userExists)
        {
            return false;
        }

        album.Id = _albums.Any() ? _albums.Max(a => a.Id) + 1 : 1;
        _albums.Add(album);

        return true;
    }

    public bool AddTour(Tour tour)
    {
        var userExists = _profiles.Any(p => p.UserId == tour.UserId);
        if (!userExists)
        {
            return false;
        }

        tour.Id = _tours.Any() ? _tours.Max(t => t.Id) + 1 : 1;
        _tours.Add(tour);

        return true;
    }
}

