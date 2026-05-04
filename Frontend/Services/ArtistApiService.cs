using System.Net.Http.Json;
using System.Text.Json;
using Frontend.Models;

namespace Frontend.Services;

public class ArtistApiService
{
    private readonly HttpClient _http;

    public ArtistApiService(HttpClient http) => _http = http;

    public async Task<List<ArtistDto>> SearchAsync(string? name, List<string>? genres, List<string>? languages, int? yearFrom, int? yearTo,bool onlyActive)
    {
        var query = new List<string>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query.Add($"name={Uri.EscapeDataString(name)}");
        }

        if (genres != null)
        {
            foreach (var g in genres)
            {
                query.Add($"genres={Uri.EscapeDataString(g)}");
            }
        }

        if (languages != null)
        {
            foreach (var l in languages)
            {
                query.Add($"languages={Uri.EscapeDataString(l)}");
            }
        }

        if (yearFrom.HasValue)
        {
            query.Add($"yearFrom={yearFrom}");
        }

        if (yearTo.HasValue)
        {
            query.Add($"yearTo={yearTo}");
        }

        if (onlyActive)
        {
            query.Add("onlyActive=true");
        }

        var url = "api/artists" + (query.Count > 0 ? "?" + string.Join("&", query) : "");
        return await _http.GetFromJsonAsync<List<ArtistDto>>(url) ?? [];
    }

    public async Task<List<string>> GetGenresAsync()
    {
        return await _http.GetFromJsonAsync<List<string>>("api/artists/genres") ?? [];
    }

    public async Task<List<string>> GetLanguagesAsync()
    {
        return await _http.GetFromJsonAsync<List<string>>("api/artists/languages") ?? [];
    }

    public async Task<ArtistDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ArtistDto>($"api/artists/{id}");
    }

    public async Task<ArtistDto?> FindSimilarArtistAsync(List<int> artistIds)
    {
        var response = await _http.PostAsJsonAsync("api/artists/find-similar", artistIds);
        if (!response.IsSuccessStatusCode)
            return null;
        
        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<ArtistDto>(json, options);
    }
}
