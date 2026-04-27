public class UserProfileService
{
    private readonly List<UserProfile> _profiles = new();
    private readonly List<Album> _albums = new();
    private readonly List<Track> _tracks = new();
    private readonly List<Tour> _tours = new();
    private readonly List<Comment> _comments = new();
    private readonly Dictionary<string, int> _commentLikes = new(); // Key: "commentId-userId" Value: 1 for like, -1 for dislike
    private readonly Dictionary<int, Dictionary<int, int>> _commentVotes = new(); // commentId -> userId -> vote (1 = like, -1 = dislike, 0 = none)

    public UserProfileService()
    {
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
            }
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
    }

    public UserProfile? GetProfile(int userId) =>
        _profiles.FirstOrDefault(p => p.UserId == userId);

    public List<Album> GetAlbumsForUser(int userId) =>
        _albums.Where(a => a.UserId == userId).ToList();

    public List<Track> GetTracksForUser(int userId) =>
        _tracks.Where(t => t.UserId == userId).ToList();

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

    public bool DeleteComment(int commentId, int currentUserId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null || comment.ProfileUserId != currentUserId)
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

