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

        var albums = _profiles.GetAlbumsForUser(userId);
        var tracks = _profiles.GetTracksForUser(userId);
        var tours = _profiles.GetToursForUser(userId);

        var dto = new
        {
            Profile = profile,
            Albums = albums,
            Tracks = tracks,
            Tours = tours
        };

        return Ok(dto);
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
}