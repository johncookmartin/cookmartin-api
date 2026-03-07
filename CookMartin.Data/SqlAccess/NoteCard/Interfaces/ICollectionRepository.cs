using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard;

namespace CookMartin.Data.SqlAccess.NoteCard.Interfaces;

public interface ICollectionRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateCollectionDto dto);
    Task<IEnumerable<CollectionDto>> GetByUserAsync(string userId);
    Task<CollectionDto?> GetByIdAsync(int collectionId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateCollectionDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int collectionId);
}
