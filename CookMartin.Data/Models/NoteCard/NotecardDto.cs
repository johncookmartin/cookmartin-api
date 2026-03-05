namespace CookMartin.Data.Models.NoteCard;

public class NotecardDto
{
    public int NotecardId { get; set; }
    public int CollectionId { get; set; }
    public string FrontDescription { get; set; } = string.Empty;
    public string BackDescription { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}
