using CookMartin.Data.Models.NoteCard.Quiz;

namespace CookMartin.NoteCard.Services;

public interface IQuizService
{
    Task<int> CreateQuizAsync(CreateQuizDto dto);
    Task<bool> DeleteQuizAsync(int quizId);
    Task<IEnumerable<QuizInstanceDto>> GetInstancesByIdAsync(int quizId);
    Task<QuizDto?> GetQuizByIdAsync(int quizId);
    Task<IEnumerable<QuizDto>> GetQuizzesByUserAsync(string userId);
    Task<bool> RecordAnswerAsnyc(RecordQuizAnswerDto dto);
}