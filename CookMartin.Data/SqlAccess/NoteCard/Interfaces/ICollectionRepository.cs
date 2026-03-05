using CookMartin.Data.Models.NoteCard;

namespace CookMartin.Data.SqlAccess.NoteCard.Interfaces;

public interface ICollectionRepository
{
    Task<int> CreateAsync(CreateCollectionDto dto);
    Task<IEnumerable<CollectionDto>> GetByUserAsync(string userId);
    Task<CollectionDto?> GetByIdAsync(int collectionId);
    Task<int> UpdateAsync(UpdateCollectionDto dto);
    Task<int> DeleteAsync(int collectionId);
}
