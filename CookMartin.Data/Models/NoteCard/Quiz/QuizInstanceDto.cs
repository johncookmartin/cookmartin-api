namespace CookMartin.Data.Models.NoteCard.Quiz;

public class QuizInstanceDto
{
    public int QuizInstanceId { get; set; }
    public int QuizId { get; set; }
    public int NotecardId { get; set; }
    public string FrontDescription { get; set; } = string.Empty;
    public string BackDescription { get; set; } = string.Empty;
    public bool? IsCorrect { get; set; }
    public DateTime? AnsweredDate { get; set; }
}
