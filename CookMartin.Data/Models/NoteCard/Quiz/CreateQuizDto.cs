namespace CookMartin.Data.Models.NoteCard.Quiz;

public class CreateQuizDto
{
    public int CollectionId { get; set; }
    public string UserId { get; set; } = "guest";
}
