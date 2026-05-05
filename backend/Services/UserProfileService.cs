public class UserProfileService
{
    private readonly AuthService _authService;
    private readonly List<UserProfile> _profiles = new();
    private readonly List<Album> _albums = new();
    private readonly List<Track> _tracks = new();
    private readonly List<Tour> _tours = new();
    private readonly List<Comment> _comments = new();
    private readonly Dictionary<string, int> _commentLikes = new(); // Key: "commentId-userId" Value: 1 for like, -1 for dislike
    private readonly Dictionary<int, Dictionary<int, int>> _commentVotes = new(); // commentId -> userId -> vote (1 = like, -1 = dislike, 0 = none)

    public UserProfileService(AuthService authService)
    {
        _authService = authService;

        // Seed demo profile data aligned with artist IDs from ArtistService (101+)
        _profiles.AddRange(new[]
        {
            new UserProfile
            {
                UserId = 101,
                DisplayName = "Andrius Mamontovas",
                Bio = "Lithuanian rock artist and songwriter.",
                DefaultLanguage = "English",
                Genres = new List<string> { "Pop", "Rock" },
                SpotifyUrl = "https://open.spotify.com/",
                YouTubeUrl = "https://www.youtube.com/"
            },
            new UserProfile
            {
                UserId = 102,
                DisplayName = "Skamp",
                Bio = "Lithuanian pop group known for upbeat English-language songs.",
                DefaultLanguage = "English",
                Genres = new List<string> { "Pop" },
                SpotifyUrl = "https://open.spotify.com/",
                YouTubeUrl = "https://www.youtube.com/"
            },
            new UserProfile
            {
                UserId = 103,
                DisplayName = "Jurga",
                Bio = "Lithuanian pop artist with melodic and introspective songwriting.",
                DefaultLanguage = "Lithuanian",
                Genres = new List<string> { "Pop" },
                SpotifyUrl = "https://open.spotify.com/",
                YouTubeUrl = "https://www.youtube.com/"
            },
            new UserProfile { UserId = 104, DisplayName = "The Beatles", Bio = "British rock band.", DefaultLanguage = "English", Genres = new List<string> { "Rock" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 105, DisplayName = "Radiohead", Bio = "English alternative rock band.", DefaultLanguage = "English", Genres = new List<string> { "Alternative" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 106, DisplayName = "Björk", Bio = "Icelandic singer and songwriter.", DefaultLanguage = "English", Genres = new List<string> { "Electronic" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 107, DisplayName = "Coldplay", Bio = "British pop rock band.", DefaultLanguage = "English", Genres = new List<string> { "Pop" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 108, DisplayName = "Stromae", Bio = "Belgian electronic music artist.", DefaultLanguage = "French", Genres = new List<string> { "Electronic" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 109, DisplayName = "Rammstein", Bio = "German industrial metal band.", DefaultLanguage = "German", Genres = new List<string> { "Metal" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 110, DisplayName = "Daft Punk", Bio = "French electronic music duo.", DefaultLanguage = "English", Genres = new List<string> { "Electronic" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 111, DisplayName = "Beyoncé", Bio = "American R&B and pop artist.", DefaultLanguage = "English", Genres = new List<string> { "R&B" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 112, DisplayName = "Kendrick Lamar", Bio = "American hip-hop artist.", DefaultLanguage = "English", Genres = new List<string> { "Hip-Hop" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 113, DisplayName = "G&G Sindikatas", Bio = "Lithuanian hip-hop group.", DefaultLanguage = "Lithuanian", Genres = new List<string> { "Hip-Hop" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 114, DisplayName = "Sigur Rós", Bio = "Icelandic post-rock band.", DefaultLanguage = "Icelandic", Genres = new List<string> { "Post-Rock" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 115, DisplayName = "Seu Jorge", Bio = "Brazilian musician and actor.", DefaultLanguage = "Portuguese", Genres = new List<string> { "MPB" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 116, DisplayName = "Adele", Bio = "British pop and soul singer.", DefaultLanguage = "English", Genres = new List<string> { "Pop" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 117, DisplayName = "Taylor Swift", Bio = "American pop artist and songwriter.", DefaultLanguage = "English", Genres = new List<string> { "Pop" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" },
            new UserProfile { UserId = 118, DisplayName = "Metallica", Bio = "American metal band.", DefaultLanguage = "English", Genres = new List<string> { "Metal" }, SpotifyUrl = "https://open.spotify.com/", YouTubeUrl = "https://www.youtube.com/" }
            }
            );

        _albums.AddRange(new[]
        {
            new Album
            {
                Id = 1,
                UserId = 101,
                Title = "First Steps",
                Year = 2015,
                Genre = "Pop",
                Description = "Debut studio album.",
                ExternalLink = "https://example.com/first-steps"
            },
            new Album
            {
                Id = 2,
                UserId = 101,
                Title = "On The Road",
                Year = 2019,
                Genre = "Rock",
                Description = "Live recordings from the first tour.",
                ExternalLink = "https://example.com/on-the-road"
            },
            new Album
            {
                Id = 3,
                UserId = 102,
                Title = "Midnight Signals",
                Year = 2020,
                Genre = "Synth-Pop",
                Description = "A neon-soaked debut full of analog synth hooks.",
                ExternalLink = "https://example.com/midnight-signals"
            },
            new Album
            {
                Id = 4,
                UserId = 102,
                Title = "Afterglow City",
                Year = 2023,
                Genre = "Electronic",
                Description = "Darker electronic themes with cinematic production.",
                ExternalLink = "https://example.com/afterglow-city"
            },
            new Album
            {
                Id = 5,
                UserId = 103,
                Title = "Coastal Letters",
                Year = 2018,
                Genre = "Folk",
                Description = "Warm acoustic debut inspired by life by the sea.",
                ExternalLink = "https://example.com/coastal-letters"
            },
            new Album
            {
                Id = 6,
                UserId = 103,
                Title = "Northern Pines",
                Year = 2022,
                Genre = "Indie",
                Description = "A textured indie-folk record with intimate vocals.",
                ExternalLink = "https://example.com/northern-pines"
            },
            new Album
            {
                Id = 7,
                UserId = 104,
                Title = "Hey Jude",
                Year = 1970,
                Genre = "Rock",
                Description = "Compilation album featuring several of The Beatles' best-known songs.",
                ExternalLink = "https://example.com/hey-jude-album"
            },
            new Album
            {
                Id = 8,
                UserId = 105,
                Title = "Pablo Honey",
                Year = 1993,
                Genre = "Alternative",
                Description = "Radiohead's debut studio album.",
                ExternalLink = "https://example.com/pablo-honey"
            },
            new Album
            {
                Id = 9,
                UserId = 106,
                Title = "Post",
                Year = 1995,
                Genre = "Electronic",
                Description = "An eclectic album mixing electronic, pop and experimental sounds.",
                ExternalLink = "https://example.com/post"
            },
            new Album
            {
                Id = 10,
                UserId = 107,
                Title = "Parachutes",
                Year = 2000,
                Genre = "Pop",
                Description = "Coldplay's debut studio album.",
                ExternalLink = "https://example.com/parachutes"
            },
            new Album
            {
                Id = 11,
                UserId = 108,
                Title = "Cheese",
                Year = 2010,
                Genre = "Electronic",
                Description = "Stromae's debut album including dance-pop and electronic influences.",
                ExternalLink = "https://example.com/cheese"
            },
            new Album
            {
                Id = 12,
                UserId = 109,
                Title = "Sehnsucht",
                Year = 1997,
                Genre = "Metal",
                Description = "Industrial metal album by Rammstein.",
                ExternalLink = "https://example.com/sehnsucht"
            },
            new Album
            {
                Id = 13,
                UserId = 110,
                Title = "Discovery",
                Year = 2001,
                Genre = "Electronic",
                Description = "Electronic album by Daft Punk featuring house, disco and synth-pop sounds.",
                ExternalLink = "https://example.com/discovery"
            },
            new Album
            {
                Id = 14,
                UserId = 111,
                Title = "I Am... Sasha Fierce",
                Year = 2008,
                Genre = "R&B",
                Description = "Beyoncé album featuring pop and R&B ballads.",
                ExternalLink = "https://example.com/i-am-sasha-fierce"
            },
            new Album
            {
                Id = 15,
                UserId = 112,
                Title = "DAMN.",
                Year = 2017,
                Genre = "Hip-Hop",
                Description = "Kendrick Lamar album blending hip-hop, spoken word and political themes.",
                ExternalLink = "https://example.com/damn"
            },
            new Album
            {
                Id = 16,
                UserId = 113,
                Title = "Tiems, kurie nieko nebijo",
                Year = 2001,
                Genre = "Hip-Hop",
                Description = "Lithuanian hip-hop album by G&G Sindikatas.",
                ExternalLink = "https://example.com/tiems-kurie-nieko-nebijo"
            },
            new Album
            {
                Id = 17,
                UserId = 114,
                Title = "Takk...",
                Year = 2005,
                Genre = "Post-Rock",
                Description = "Post-rock album by Sigur Rós.",
                ExternalLink = "https://example.com/takk"
            },
            new Album
            {
                Id = 18,
                UserId = 115,
                Title = "The Life Aquatic Studio Sessions",
                Year = 2005,
                Genre = "MPB",
                Description = "Seu Jorge album featuring Portuguese interpretations of classic songs.",
                ExternalLink = "https://example.com/life-aquatic"
            },
            new Album
            {
                Id = 19,
                UserId = 116,
                Title = "21",
                Year = 2011,
                Genre = "Pop",
                Description = "Adele album featuring soul and pop ballads.",
                ExternalLink = "https://example.com/21"
            },
            new Album
            {
                Id = 20,
                UserId = 117,
                Title = "1989",
                Year = 2014,
                Genre = "Pop",
                Description = "Taylor Swift pop album with synth-pop production.",
                ExternalLink = "https://example.com/1989"
            },
            new Album
            {
                Id = 21,
                UserId = 118,
                Title = "Metallica",
                Year = 1991,
                Genre = "Metal",
                Description = "Also known as The Black Album.",
                ExternalLink = "https://example.com/metallica"
            }
        });

                _tracks.AddRange(new[]
                {
            new Track
            {
                Id = 1,
                UserId = 101,
                AlbumId = 1,
                Title = "Morning Lights",
                Genre = "Pop",
                Language = "English",
                BeatsPerMinute = 118,
                Length = TimeSpan.FromMinutes(3.5),
                ExternalLink = "https://example.com/morning-lights"
            },
            new Track
            {
                Id = 2,
                UserId = 101,
                AlbumId = 1,
                Title = "City Nights",
                Genre = "Pop",
                Language = "English",
                BeatsPerMinute = 124,
                Length = TimeSpan.FromMinutes(4),
                ExternalLink = "https://example.com/city-nights"
            },
            new Track
            {
                Id = 3,
                UserId = 101,
                AlbumId = 2,
                Title = "Miles Ahead",
                Genre = "Rock",
                Language = "English",
                BeatsPerMinute = 132,
                Length = TimeSpan.FromMinutes(5),
                ExternalLink = "https://example.com/miles-ahead"
            },
            new Track
            {
                Id = 4,
                UserId = 102,
                AlbumId = 3,
                Title = "Glass Neon",
                Genre = "Synth-Pop",
                Language = "English",
                BeatsPerMinute = 126,
                Length = TimeSpan.FromMinutes(3.8),
                ExternalLink = "https://example.com/glass-neon"
            },
            new Track
            {
                Id = 5,
                UserId = 102,
                AlbumId = 3,
                Title = "Harbor Lights",
                Genre = "Synth-Pop",
                Language = "English",
                BeatsPerMinute = 122,
                Length = TimeSpan.FromMinutes(4.1),
                ExternalLink = "https://example.com/harbor-lights"
            },
            new Track
            {
                Id = 6,
                UserId = 102,
                AlbumId = 4,
                Title = "Soft Static",
                Genre = "Electronic",
                Language = "English",
                BeatsPerMinute = 110,
                Length = TimeSpan.FromMinutes(4.4),
                ExternalLink = "https://example.com/soft-static"
            },
            new Track
            {
                Id = 7,
                UserId = 103,
                AlbumId = 5,
                Title = "Paper Boats",
                Genre = "Folk",
                Language = "Lithuanian",
                BeatsPerMinute = 94,
                Length = TimeSpan.FromMinutes(3.2),
                ExternalLink = "https://example.com/paper-boats"
            },
            new Track
            {
                Id = 8,
                UserId = 103,
                AlbumId = 6,
                Title = "Amber Wind",
                Genre = "Indie",
                Language = "Lithuanian",
                BeatsPerMinute = 102,
                Length = TimeSpan.FromMinutes(4.0),
                ExternalLink = "https://example.com/amber-wind"
            },
            new Track
            {
                Id = 9,
                UserId = 104,
                AlbumId = 7,
                Title = "Hey Jude",
                Genre = "Rock",
                Language = "English",
                BeatsPerMinute = 74,
                Length = TimeSpan.FromMinutes(7.1),
                ExternalLink = "https://example.com/hey-jude"
            },
            new Track
            {
                Id = 10,
                UserId = 105,
                AlbumId = 8,
                Title = "Creep",
                Genre = "Alternative",
                Language = "English",
                BeatsPerMinute = 92,
                Length = TimeSpan.FromMinutes(3.9),
                ExternalLink = "https://example.com/creep"
            },
            new Track
            {
                Id = 11,
                UserId = 106,
                AlbumId = 9,
                Title = "Army of Me",
                Genre = "Electronic",
                Language = "English",
                BeatsPerMinute = 87,
                Length = TimeSpan.FromMinutes(3.9),
                ExternalLink = "https://example.com/army-of-me"
            },
            new Track
            {
                Id = 12,
                UserId = 107,
                AlbumId = 10,
                Title = "Yellow",
                Genre = "Pop",
                Language = "English",
                BeatsPerMinute = 88,
                Length = TimeSpan.FromMinutes(4.4),
                ExternalLink = "https://example.com/yellow"
            },
            new Track
            {
                Id = 13,
                UserId = 108,
                AlbumId = 11,
                Title = "Alors on danse",
                Genre = "Electronic",
                Language = "French",
                BeatsPerMinute = 120,
                Length = TimeSpan.FromMinutes(3.4),
                ExternalLink = "https://example.com/alors-on-danse"
            },
            new Track
            {
                Id = 14,
                UserId = 109,
                AlbumId = 12,
                Title = "Du Hast",
                Genre = "Metal",
                Language = "German",
                BeatsPerMinute = 125,
                Length = TimeSpan.FromMinutes(3.9),
                ExternalLink = "https://example.com/du-hast"
            },
            new Track
            {
                Id = 15,
                UserId = 110,
                AlbumId = 13,
                Title = "One More Time",
                Genre = "Electronic",
                Language = "English",
                BeatsPerMinute = 123,
                Length = TimeSpan.FromMinutes(5.3),
                ExternalLink = "https://example.com/one-more-time"
            },
            new Track
            {
                Id = 16,
                UserId = 111,
                AlbumId = 14,
                Title = "Halo",
                Genre = "R&B",
                Language = "English",
                BeatsPerMinute = 80,
                Length = TimeSpan.FromMinutes(4.3),
                ExternalLink = "https://example.com/halo"
            },
            new Track
            {
                Id = 17,
                UserId = 112,
                AlbumId = 15,
                Title = "HUMBLE.",
                Genre = "Hip-Hop",
                Language = "English",
                BeatsPerMinute = 150,
                Length = TimeSpan.FromMinutes(2.9),
                ExternalLink = "https://example.com/humble"
            },
            new Track
            {
                Id = 18,
                UserId = 113,
                AlbumId = 16,
                Title = "Tomas",
                Genre = "Hip-Hop",
                Language = "Lithuanian",
                BeatsPerMinute = 96,
                Length = TimeSpan.FromMinutes(3.7),
                ExternalLink = "https://example.com/tomas"
            },
            new Track
            {
                Id = 19,
                UserId = 114,
                AlbumId = 17,
                Title = "Hoppípolla",
                Genre = "Post-Rock",
                Language = "Icelandic",
                BeatsPerMinute = 82,
                Length = TimeSpan.FromMinutes(4.5),
                ExternalLink = "https://example.com/hoppipolla"
            },
            new Track
            {
                Id = 20,
                UserId = 115,
                AlbumId = 18,
                Title = "Life on Mars?",
                Genre = "MPB",
                Language = "Portuguese",
                BeatsPerMinute = 104,
                Length = TimeSpan.FromMinutes(3.6),
                ExternalLink = "https://example.com/life-on-mars"
            },
            new Track
            {
                Id = 21,
                UserId = 116,
                AlbumId = 19,
                Title = "Rolling in the Deep",
                Genre = "Pop",
                Language = "English",
                BeatsPerMinute = 105,
                Length = TimeSpan.FromMinutes(3.8),
                ExternalLink = "https://example.com/rolling-in-the-deep"
            },
            new Track
            {
                Id = 22,
                UserId = 117,
                AlbumId = 20,
                Title = "Blank Space",
                Genre = "Pop",
                Language = "English",
                BeatsPerMinute = 96,
                Length = TimeSpan.FromMinutes(3.9),
                ExternalLink = "https://example.com/blank-space"
            },
            new Track
            {
                Id = 23,
                UserId = 118,
                AlbumId = 21,
                Title = "Enter Sandman",
                Genre = "Metal",
                Language = "English",
                BeatsPerMinute = 123,
                Length = TimeSpan.FromMinutes(5.5),
                ExternalLink = "https://example.com/enter-sandman"
            }
        });

        _tours.AddRange(new[]
        {
            new Tour
            {
                Id = 1,
                UserId = 101,
                Name = "Spring Lights Tour",
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow.AddMonths(-5),
                Location = "Baltic States"
            },
            new Tour
            {
                Id = 2,
                UserId = 101,
                Name = "Summer Festivals",
                StartDate = DateTime.UtcNow.AddMonths(1),
                EndDate = DateTime.UtcNow.AddMonths(2),
                Location = "Europe"
            },
            new Tour
            {
                Id = 3,
                UserId = 102,
                Name = "Neon Nights",
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(-2),
                Location = "UK"
            },
            new Tour
            {
                Id = 4,
                UserId = 102,
                Name = "Afterglow Live",
                StartDate = DateTime.UtcNow.AddMonths(2),
                EndDate = DateTime.UtcNow.AddMonths(3),
                Location = "Scandinavia"
            },
            new Tour
            {
                Id = 5,
                UserId = 103,
                Name = "Forest Sessions",
                StartDate = DateTime.UtcNow.AddMonths(-8),
                EndDate = DateTime.UtcNow.AddMonths(-7),
                Location = "Baltic States"
            },
            new Tour
            {
                Id = 6,
                UserId = 103,
                Name = "Coastal Evenings",
                StartDate = DateTime.UtcNow.AddMonths(4),
                EndDate = DateTime.UtcNow.AddMonths(5),
                Location = "Northern Europe"
            }
        });

        EnsureProfilesExistForAllAccounts();
    }

    public UserProfile? GetProfile(int userId)
    {
        EnsureProfileExistsForUser(userId);
        return _profiles.FirstOrDefault(p => p.UserId == userId);
    }

    public List<Album> GetAlbumsForUser(int userId) =>
        _albums.Where(a => a.UserId == userId).ToList();

    public List<Track> GetTracksForUser(int userId) =>
        _tracks.Where(t => t.UserId == userId).ToList();

    public void EnsureProfileExistsForUser(int userId)
    {
        if (_profiles.Any(p => p.UserId == userId))
        {
            return;
        }

        var account = _authService.GetById(userId);
        if (account == null)
        {
            return;
        }

        _profiles.Add(new UserProfile
        {
            UserId = account.Id,
            DisplayName = account.Username,
            Bio = "",
            DefaultLanguage = "",
            Genres = new List<string>(),
            SpotifyUrl = "",
            YouTubeUrl = ""
        });
    }

    private void EnsureProfilesExistForAllAccounts()
    {
        foreach (var account in _authService.GetAllUsers())
        {
            EnsureProfileExistsForUser(account.Id);
        }
    }

    public List<TrackSearchResult> SearchTracks(string? name, List<string>? genres, int? bpmFrom, int? bpmTo)
    {
        var q = _tracks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            q = q.Where(t => t.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (genres != null && genres.Count > 0)
        {
            q = q.Where(t => genres.Contains(t.Genre));
        }

        if (bpmFrom.HasValue)
        {
            q = q.Where(t => t.BeatsPerMinute.HasValue && t.BeatsPerMinute.Value >= bpmFrom.Value);
        }

        if (bpmTo.HasValue)
        {
            q = q.Where(t => t.BeatsPerMinute.HasValue && t.BeatsPerMinute.Value <= bpmTo.Value);
        }

        var profileNamesByUserId = _profiles.ToDictionary(p => p.UserId, p => p.DisplayName);
        var albumTitlesById = _albums.ToDictionary(a => a.Id, a => a.Title);

        return q
            .ToList()
            .Select(t =>
            {
                profileNamesByUserId.TryGetValue(t.UserId, out var artistName);

                string? albumTitle = null;
                if (t.AlbumId.HasValue)
                {
                    albumTitlesById.TryGetValue(t.AlbumId.Value, out albumTitle);
                }

                return new TrackSearchResult
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    ArtistName = artistName ?? "Unknown artist",
                    AlbumId = t.AlbumId,
                    AlbumTitle = albumTitle,
                    Title = t.Title,
                    Genre = t.Genre,
                    Language = t.Language,
                    BeatsPerMinute = t.BeatsPerMinute,
                    Length = t.Length,
                    ExternalLink = t.ExternalLink
                };
            })
            .OrderBy(t => t.Title)
            .ToList();
    }

    public List<string> GetTrackGenres()
    {
        return _tracks
            .Select(t => t.Genre)
            .Where(g => !string.IsNullOrWhiteSpace(g))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(g => g)
            .ToList();
    }

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

    public List<Comment> GetCommentsForProfile(int profileUserId) =>
        _comments.Where(c => c.ProfileUserId == profileUserId)
                  .OrderByDescending(c => c.CreatedAt)
                  .ToList();

    public bool AddComment(Comment comment)
    {
        comment.Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1;
        comment.CreatedAt = DateTime.UtcNow;
        _comments.Add(comment);
        return true;
    }

    public bool DeleteComment(int commentId, int currentUserId, bool isAdmin = false)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            return false;
        }

        // Allow deletion if user is the profile owner or if user is admin
        if (comment.ProfileUserId != currentUserId && !isAdmin)
        {
            return false;
        }

        _comments.Remove(comment);
        return true;
    }

    public bool LikeComment(int commentId, int userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
            return false;

        if (!_commentVotes.ContainsKey(commentId))
            _commentVotes[commentId] = new();

        if (_commentVotes[commentId].TryGetValue(userId, out var existingVote))
        {
            if (existingVote == 1) // Already liked
                return true;
            
            if (existingVote == -1) // Changing from dislike to like
                comment.Dislikes--;
            
            _commentVotes[commentId][userId] = 1;
            comment.Likes++;
        }
        else
        {
            _commentVotes[commentId][userId] = 1;
            comment.Likes++;
        }

        return true;
    }

    public bool DislikeComment(int commentId, int userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
            return false;

        if (!_commentVotes.ContainsKey(commentId))
            _commentVotes[commentId] = new();

        if (_commentVotes[commentId].TryGetValue(userId, out var existingVote))
        {
            if (existingVote == -1) // Already disliked
                return true;

            if (existingVote == 1) // Changing from like to dislike
                comment.Likes--;

            _commentVotes[commentId][userId] = -1;
            comment.Dislikes++;
        }
        else
        {
            _commentVotes[commentId][userId] = -1;
            comment.Dislikes++;
        }

        return true;
    }

    public int? GetUserVote(int commentId, int userId)
    {
        if (_commentVotes.TryGetValue(commentId, out var votes))
        {
            if (votes.TryGetValue(userId, out var vote))
                return vote;
        }
        return null;
    }
}

