using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly ArtistService _svc;
    public ArtistsController(ArtistService svc) => _svc = svc;

    [HttpGet]
    public IActionResult Search(
        [FromQuery] string? name,
        [FromQuery] List<string>? genres,
        [FromQuery] List<string>? languages,
        [FromQuery] int? yearFrom,
        [FromQuery] int? yearTo,
        [FromQuery] bool onlyActive = false)
    {
        return Ok(_svc.Search(name, genres, languages, yearFrom, yearTo, onlyActive));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var artist = _svc.GetById(id);
        return artist is null ? NotFound() : Ok(artist);
    }

    [HttpGet("genres")]
    public IActionResult GetGenres() => Ok(_svc.GetGenres());

    [HttpGet("languages")]
    public IActionResult GetLanguages() => Ok(_svc.GetLanguages());
}