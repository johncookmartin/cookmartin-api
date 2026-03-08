namespace CookMartin.Data.Models.NoteCard.Quiz;

public class RecordQuizAnswerDto
{
    public int InstanceId { get; set; }
    public int QuizId { get; set; }
    public bool IsCorrect { get; set; }
}
