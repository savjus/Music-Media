using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TracksController : ControllerBase
{
    private readonly UserProfileService _profiles;

    public TracksController(UserProfileService profiles)
    {
        _profiles = profiles;
    }

    [HttpGet]
    public IActionResult Search(
        [FromQuery] string? name,
        [FromQuery] List<string>? genres,
        [FromQuery] int? bpmFrom,
        [FromQuery] int? bpmTo)
    {
        return Ok(_profiles.SearchTracks(name, genres, bpmFrom, bpmTo));
    }

    [HttpGet("genres")]
    public IActionResult GetGenres()
    {
        return Ok(_profiles.GetTrackGenres());
    }
}
