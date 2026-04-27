namespace Frontend.Models;

public class CommentDto
{
    public int Id { get; set; }
    public int ProfileUserId { get; set; }
    public int AuthorUserId { get; set; }
    public string AuthorName { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
}