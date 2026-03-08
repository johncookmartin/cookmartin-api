using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Notecard;

namespace CookMartin.Data.SqlAccess.NoteCard.Interfaces;

public interface INotecardRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateNotecardDto dto);
    Task<IEnumerable<NotecardDto>> GetByCollectionAsync(int collectionId);
    Task<NotecardDto?> GetByIdAsync(int notecardId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateNotecardDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int notecardId);
    Task<IEnumerable<QuizNotecardDto>> GetNotecardsByQuizIdAsync(int quizId);
}
