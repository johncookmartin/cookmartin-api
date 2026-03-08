namespace CookMartin.Data.Models.NoteCard.Quiz;

public class RecordQuizAnswerDto
{
    public int InstanceId { get; set; }
    public int quizId { get; set; }
    public bool IsCorrect { get; set; }
}
