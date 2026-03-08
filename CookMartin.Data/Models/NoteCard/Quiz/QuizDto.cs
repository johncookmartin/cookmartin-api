namespace CookMartin.Data.Models.NoteCard.Quiz;

public class QuizDto
{
    public int QuizId { get; set; }
    public int CollectionId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime QuizDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? Score { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}
