using CookMartin.Data.Models.NoteCard.Collection;
using CookMartin.Data.Models.NoteCard.Notecard;

namespace CookMartin.NoteCard.Services.Interfaces;

public interface INotecardService
{
    Task<NotecardDto> CreateNotecardAsync(CreateNotecardDto dto);
    Task<NotecardDto?> GetNotecardByIdAsync(int notecardId);
    Task<IEnumerable<NotecardDto>> GetNotecardsByCollectionAsync(CollectionDto collection);
    Task<bool> UpdateNotecardAsync(int notecardId, UpdateNotecardDto dto);
    Task<bool> DeleteNotecardAsync(int notecardId);
    Task<QuizNotecardDto?> GetNextNoteCardAsync(int quizId);
}
