using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Frontend.Services;

public class ProfileApiService
{
    private readonly IHttpClientFactory _factory;
    private readonly AuthService _auth;

    public bool editing = false;
    public event Func<Task>? OnSave;
    public event Action? OnDiscard;
    public event Func<bool, Task>? OnEditingChanged;
    public event Action? OnProfileChanged;

    public ProfileApiService(IHttpClientFactory factory, AuthService auth)
    {
        _factory = factory;
        _auth = auth;
    }

    public async Task<ProfileResponseDto?> GetMyProfileAsync()
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.GetAsync("api/profile/me");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<ProfileResponseDto>();
    }

    public async Task<bool> UpdateMyProfileAsync(UserProfileDto profile)
    {
        var client = _factory.CreateClient("auth");
        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PutAsJsonAsync("api/profile/me", profile);
        return response.IsSuccessStatusCode;
    }

    public async Task Save()
    {
        if (OnSave is not null)
        {
            await OnSave.Invoke();
        }
        OnProfileChanged?.Invoke();
    }

    public void Discard()
    {
        OnDiscard?.Invoke();
    }

    public async Task SwitchEditing()
    {
        editing = !editing;
        if (OnEditingChanged is not null)
        {
            await OnEditingChanged.Invoke(editing);
        }
    }

    public async Task<TrackDto?> AddTrackAsync(TrackDto track)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync("api/profile/me/tracks", track);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<TrackDto>();
    }

    public async Task<AlbumDto?> AddAlbumAsync(AlbumDto album)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync("api/profile/me/albums", album);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<AlbumDto>();
    }

    public async Task<TourDto?> AddTourAsync(TourDto tour)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync("api/profile/me/tours", tour);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<TourDto>();
    }

    public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync("api/auth/change-password", new
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        });

        return response.IsSuccessStatusCode;
    }
}

