namespace CookMartin.API.Models.NoteCard;

public class RecordQuizAnswerRequest
{
    public int NotecardId { get; set; }
    public bool IsCorrect { get; set; }
}
