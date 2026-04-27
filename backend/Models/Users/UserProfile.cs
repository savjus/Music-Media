public class UserProfile
{
    public int UserId { get; set; }
    public string DisplayName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string DefaultLanguage { get; set; } = "";
    public List<string> Genres { get; set; } = new();
    public string SpotifyUrl { get; set; } = "";
    public string YouTubeUrl { get; set; } = "";
}