public class Tour
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = "";

    public bool IsPast => EndDate < DateTime.UtcNow.Date;
}