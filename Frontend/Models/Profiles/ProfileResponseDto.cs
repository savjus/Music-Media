namespace Frontend.Models;

public class ProfileResponseDto
{
    public UserProfileDto Profile { get; set; } = new();
    public List<AlbumDto> Albums { get; set; } = new();
    public List<TrackDto> Tracks { get; set; } = new();
    public List<TourDto> Tours { get; set; } = new();
    public List<CommentDto> Comments { get; set; } = new();
}