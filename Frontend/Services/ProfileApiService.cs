using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Frontend.Services;

public class ProfileApiService
{
    private readonly IHttpClientFactory _factory;
    private readonly AuthService _auth;

    public bool editing = false;

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
}

