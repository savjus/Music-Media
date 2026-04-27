using System.Net.Http.Headers;
using System.Net.Http.Json;
using Frontend.Models;


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

    public async Task<ProfileResponseDto?> GetProfileByUserIdAsync(int userId)
    {
        var client = _factory.CreateClient("auth");
        var response = await client.GetAsync($"api/profile/{userId}");

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

    public async Task<bool> DeleteAccountAsync(string password)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync("api/auth/delete-account", new
        {
            Password = password
        });

        return response.IsSuccessStatusCode;
    }

    public async Task<List<CommentDto>?> GetCommentsAsync(int userId)
    {
        var client = _factory.CreateClient("auth");
        var response = await client.GetAsync($"api/profile/{userId}/comments");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<CommentDto>>();
    }

    public async Task<(CommentDto? Comment, string? Error)> AddCommentWithErrorAsync(int userId, string content)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var request = new { Content = content };
        var response = await client.PostAsJsonAsync($"api/profile/{userId}/comments", request);

        if (!response.IsSuccessStatusCode)
        {
            string? errorMessage = null;
            try
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                errorMessage = errorContent ?? response.ReasonPhrase ?? "Unknown error";
            }
            catch
            {
                errorMessage = response.ReasonPhrase ?? "Unknown error";
            }
            return (null, errorMessage);
        }

        var result = await response.Content.ReadFromJsonAsync<CommentDto>();
        return (result, null);
    }

    public async Task<CommentDto?> AddCommentAsync(int userId, string content)
    {
        var (comment, _) = await AddCommentWithErrorAsync(userId, content);
        return comment;
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.DeleteAsync($"api/profile/comments/{commentId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> LikeCommentAsync(int commentId)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync($"api/profile/comments/{commentId}/like", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DislikeCommentAsync(int commentId)
    {
        var client = _factory.CreateClient("auth");

        if (!string.IsNullOrEmpty(_auth.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.Token);
        }

        var response = await client.PostAsJsonAsync($"api/profile/comments/{commentId}/dislike", new { });
        return response.IsSuccessStatusCode;
    }
}

