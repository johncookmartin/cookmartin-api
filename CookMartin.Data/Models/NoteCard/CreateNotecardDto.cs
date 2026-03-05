namespace CookMartin.Data.Models.NoteCard;

public class CreateNotecardDto
{
    public int CollectionId { get; set; }
    public string FrontDescription { get; set; } = string.Empty;
    public string BackDescription { get; set; } = string.Empty;
}
