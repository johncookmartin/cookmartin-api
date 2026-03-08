using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Quiz;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public interface IQuizRepository
{
    Task<int> CompleteAsync(IWriteDb writeDb, int quizId);
    Task<int> CreateAsync(IWriteDb writeDb, CreateQuizDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int quizId);
    Task<QuizDto?> GetByIdAsync(int quizId);
    Task<IEnumerable<QuizDto>> GetByUserAsync(string userID);
    Task<IEnumerable<QuizInstanceDto>> GetInstancesByIdAsync(int quizId);
    Task<int> RecordAnswerAsync(IWriteDb writeDb, RecordQuizAnswerDto dto);
}