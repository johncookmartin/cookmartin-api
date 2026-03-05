using CookMartin.Data.Models.NoteCard;

namespace CookMartin.Data.SqlAccess.NoteCard.Interfaces;

public interface INotecardRepository
{
    Task<int> CreateAsync(CreateNotecardDto dto);
    Task<IEnumerable<NotecardDto>> GetByCollectionAsync(int collectionId);
    Task<NotecardDto?> GetByIdAsync(int notecardId);
    Task<int> UpdateAsync(UpdateNotecardDto dto);
    Task<int> DeleteAsync(int notecardId);
}
