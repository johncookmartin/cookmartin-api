namespace CookMartin.Data.Models.NoteCard.Quiz;

public class RecordQuizAnswerDto
{
    public int NotecardId { get; set; }
    public int QuizId { get; set; }
    public bool IsCorrect { get; set; }
}
