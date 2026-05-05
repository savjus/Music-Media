using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserProfileService _profiles;
    private readonly AuthService _authService;

    public ProfileController(UserProfileService profiles, AuthService authService)
    {
        _profiles = profiles;
        _authService = authService;
    }

    public class ProfileResponse
    {
        public UserProfile? Profile { get; set; }
        public List<Album> Albums { get; set; } = new();
        public List<Track> Tracks { get; set; } = new();
        public List<Tour> Tours { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }

        return userId;
    }

    [AllowAnonymous]
    [HttpGet("{userId:int}")]
    public IActionResult GetByUserId(int userId)
    {
        var profile = _profiles.GetProfile(userId);
        var comments = _profiles.GetCommentsForProfile(userId);

        var response = new ProfileResponse
        {
            Profile = profile,
            Albums = profile != null ? _profiles.GetAlbumsForUser(userId) : new(),
            Tracks = profile != null ? _profiles.GetTracksForUser(userId) : new(),
            Tours = profile != null ? _profiles.GetToursForUser(userId) : new(),
            Comments = comments
        };

        return Ok(response);
    }

    [HttpGet("me")]
    public IActionResult GetMe()
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var profile = _profiles.GetProfile(userId.Value);
        if (profile == null)
        {
            return NotFound();
        }

        var response = new ProfileResponse
        {
            Profile = profile,
            Albums = _profiles.GetAlbumsForUser(userId.Value),
            Tracks = _profiles.GetTracksForUser(userId.Value),
            Tours = _profiles.GetToursForUser(userId.Value),
            Comments = _profiles.GetCommentsForProfile(userId.Value)
        };

        return Ok(response);
    }

    [HttpPut("me")]
    public IActionResult UpdateMe([FromBody] UserProfile updatedProfile)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        updatedProfile.UserId = userId.Value;
        var success = _profiles.UpdateProfile(updatedProfile);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("me/tracks")]
    public IActionResult AddTrack([FromBody] Track track)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        track.UserId = userId.Value;

        var success = _profiles.AddTrack(track);

        if (!success)
        {
            return BadRequest();
        }

        return Ok(track);
    }

    [HttpPost("me/albums")]
    public IActionResult AddAlbum([FromBody] Album album)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        album.UserId = userId.Value;

        var success = _profiles.AddAlbum(album);

        if (!success)
        {
            return BadRequest();
        }

        return Ok(album);
    }

    [HttpPost("me/tours")]
    public IActionResult AddTour([FromBody] Tour tour)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        tour.UserId = userId.Value;

        var success = _profiles.AddTour(tour);

        if (!success)
        {
            return BadRequest();
        }

        return Ok(tour);
    }

    [HttpGet("favorites")]
    public IActionResult GetFavorites()
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        return Ok(_profiles.GetFavoriteArtistIds(userId.Value));
    }

    [HttpGet("favorites/{artistId:int}")]
    public IActionResult IsFavorite(int artistId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        return Ok(_profiles.IsFavoriteArtist(userId.Value, artistId));
    }

    [HttpPost("favorites/{artistId:int}")]
    public IActionResult AddFavorite(int artistId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        _profiles.AddFavoriteArtist(userId.Value, artistId);
        return Ok();
    }

    [HttpDelete("favorites/{artistId:int}")]
    public IActionResult RemoveFavorite(int artistId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        _profiles.RemoveFavoriteArtist(userId.Value, artistId);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("{userId:int}/comments")]
    public IActionResult GetComments(int userId)
    {
        var comments = _profiles.GetCommentsForProfile(userId);
        return Ok(comments);
    }

    [HttpPost("{userId:int}/comments")]
    public IActionResult AddComment(int userId, [FromBody] CreateCommentRequest request)
    {
        var currentUserId = GetCurrentUserId();

        if (currentUserId == null)
        {
            return Unauthorized("Invalid or missing authentication token.");
        }

        if (request == null || string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest("Comment content cannot be empty.");
        }

        if (ProfanityFilter.ContainsProfanity(request.Content))
        {
            return BadRequest(ProfanityFilter.GetProfanityErrorMessage());
        }

        var comment = new Comment
        {
            ProfileUserId = userId,
            AuthorUserId = currentUserId.Value,
            Content = request.Content.Trim()
        };

        var authorProfile = _profiles.GetProfile(currentUserId.Value);
        comment.AuthorName = authorProfile?.DisplayName ?? "Anonymous";

        var success = _profiles.AddComment(comment);

        if (!success)
        {
            return BadRequest("Failed to add comment.");
        }

        return Ok(comment);
    }

    public class CreateCommentRequest
    {
        public string? Content { get; set; }
    }

    [HttpDelete("comments/{commentId:int}")]
    public IActionResult DeleteComment(int commentId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var user = _authService.GetById(userId.Value);
        bool isAdmin = user?.IsAdmin ?? false;

        var success = _profiles.DeleteComment(commentId, userId.Value, isAdmin);

        if (!success)
        {
            return Forbid();
        }

        return NoContent();
    }

    [HttpPost("comments/{commentId:int}/like")]
    public IActionResult LikeComment(int commentId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var success = _profiles.LikeComment(commentId, userId.Value);

        if (!success)
        {
            return NotFound("Comment not found.");
        }

        return Ok();
    }

    [HttpPost("comments/{commentId:int}/dislike")]
    public IActionResult DislikeComment(int commentId)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var success = _profiles.DislikeComment(commentId, userId.Value);

        if (!success)
        {
            return NotFound("Comment not found.");
        }

        return Ok();
    }
}