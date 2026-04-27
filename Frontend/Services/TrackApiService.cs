using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class TrackApiService
{
    private readonly HttpClient _http;

    public TrackApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TrackSearchDto>> SearchAsync(
        string? name,
        List<string>? genres,
        int? bpmFrom,
        int? bpmTo)
    {
        var query = new List<string>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query.Add($"name={Uri.EscapeDataString(name)}");
        }

        if (genres != null)
        {
            foreach (var genre in genres)
            {
                query.Add($"genres={Uri.EscapeDataString(genre)}");
            }
        }

        if (bpmFrom.HasValue)
        {
            query.Add($"bpmFrom={bpmFrom.Value}");
        }

        if (bpmTo.HasValue)
        {
            query.Add($"bpmTo={bpmTo.Value}");
        }

        var url = "api/tracks" + (query.Count > 0 ? "?" + string.Join("&", query) : "");
        return await _http.GetFromJsonAsync<List<TrackSearchDto>>(url) ?? [];
    }

    public async Task<List<string>> GetGenresAsync()
    {
        return await _http.GetFromJsonAsync<List<string>>("api/tracks/genres") ?? [];
    }
}
