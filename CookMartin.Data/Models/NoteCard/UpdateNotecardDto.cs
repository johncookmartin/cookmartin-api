namespace CookMartin.Data.Models.NoteCard;

public class UpdateNotecardDto
{
    public int NotecardId { get; set; }
    public string FrontDescription { get; set; } = string.Empty;
    public string BackDescription { get; set; } = string.Empty;
}
