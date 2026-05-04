using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Frontend.Models;

namespace Frontend.Services;

public class AuthService
{
    private readonly IHttpClientFactory _factory;

    public AuthService(IHttpClientFactory factory) => _factory = factory;

    public string? Token { get; private set; }
    public UserInfo? CurrentUser { get; private set; }

    public bool IsLoggedIn => Token != null && !IsTokenExpired(Token);

    public event Action? OnAuthChanged;

    public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
    {
        var client = _factory.CreateClient("auth");
        var response = await client.PostAsJsonAsync("api/auth/login", new { email, password, rememberMe });

        if (!response.IsSuccessStatusCode) 
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result?.Token is null) 
        {
            return false;
        }

        Token = result.Token;
        CurrentUser = ParseUserInfo(Token);
        OnAuthChanged?.Invoke();
        return true;
    }

    public async Task<bool> RegisterAsync(string username, string email, string password)
    {
        var client = _factory.CreateClient("auth");
        var response = await client.PostAsJsonAsync("api/auth/register", new { username, email, password });
        return response.IsSuccessStatusCode;
    }

    public bool TryRestoreToken(string token)
    {
        if (IsLoggedIn) return true;        
        if (IsTokenExpired(token)) return false;

        Token = token;
        CurrentUser = ParseUserInfo(token);
        OnAuthChanged?.Invoke();
        return true;
    }

    public void Logout()
    {
        Token = null;
        CurrentUser = null;
        OnAuthChanged?.Invoke();
    }

    private static bool IsTokenExpired(string token)
    {
        try
        {
            var exp = ParsePayload(token).GetProperty("exp").GetInt64();
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= exp;
        }
        catch 
        { 
            return true;
        }
    }

    private static UserInfo? ParseUserInfo(string token)
    {
        try
        {
            var payload = ParsePayload(token);
            var userIdStr = TryGetString(payload, "sub") ?? TryGetString(payload, "userid");
            int.TryParse(userIdStr, out var userId);
            
            return new UserInfo
            {
                UserId = userId,
                Username = TryGetString(payload, "name") ?? "",
                Email = TryGetString(payload, "email") ?? ""
            };
        }
        catch
        {
            return null; 
        }
    }

    private static JsonElement ParsePayload(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3) 
        {
            throw new FormatException("Invalid JWT format.");
        }

        var base64 = parts[1].Replace('-', '+').Replace('_', '/');
        base64 = base64.PadRight((base64.Length + 3) / 4 * 4, '=');

        var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        return JsonDocument.Parse(json).RootElement;
    }

    private static string? TryGetString(JsonElement el, string prop)
    {
        return el.TryGetProperty(prop, out var v) ? v.GetString() : null;
    }

    public int UserId => CurrentUser?.UserId ?? 0;

    // DTO backend responsams 
    private class LoginResult
    {
        public string Token    { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email    { get; set; } = "";
    }
}
