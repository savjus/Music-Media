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

    public ProfileController(UserProfileService profiles)
    {
        _profiles = profiles;
    }

    public class ProfileResponse
    {
        public UserProfile? Profile { get; set; }
        public List<Album> Albums { get; set; } = new();
        public List<Track> Tracks { get; set; } = new();
        public List<Tour> Tours { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var profile = _profiles.GetProfile(userId);
        if (profile == null)
        {
            return NotFound();
        }

        var response = new ProfileResponse
        {
            Profile = profile,
            Albums = _profiles.GetAlbumsForUser(userId),
            Tracks = _profiles.GetTracksForUser(userId),
            Tours = _profiles.GetToursForUser(userId),
            Comments = _profiles.GetCommentsForProfile(userId)
        };

        return Ok(response);
    }

    [HttpPut("me")]
    public IActionResult UpdateMe([FromBody] UserProfile updatedProfile)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        updatedProfile.UserId = userId;
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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        track.UserId = userId;

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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        album.UserId = userId;

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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        tour.UserId = userId;

        var success = _profiles.AddTour(tour);

        if (!success)
        {
            return BadRequest();
        }

        return Ok(tour);
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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var currentUserId))
        {
            return Unauthorized("Invalid or missing authentication token.");
        }

        // Validate comment content
        if (request == null || string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest("Comment content cannot be empty.");
        }

        var comment = new Comment
        {
            ProfileUserId = userId,
            AuthorUserId = currentUserId,
            Content = request.Content.Trim()
        };

        // Set author name from current user's profile
        var authorProfile = _profiles.GetProfile(currentUserId);
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
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub ||
            c.Type == "sub" ||
            c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var success = _profiles.DeleteComment(commentId, userId);

        if (!success)
        {
            return Forbid();
        }

        return NoContent();
    }
}