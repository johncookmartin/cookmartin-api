using CookMartin.Data.Models.NoteCard.Notecard;

namespace CookMartin.NoteCard.Services.Interfaces;

public interface INotecardService
{
    Task<NotecardDto> CreateNotecardAsync(CreateNotecardDto dto);
    Task<NotecardDto?> GetNotecardByIdAsync(int notecardId);
    Task<IEnumerable<NotecardDto>> GetNotecardsByCollectionAsync(int collectionId);
    Task<bool> UpdateNotecardAsync(int notecardId, UpdateNotecardDto dto);
    Task<bool> DeleteNotecardAsync(int notecardId);
}
