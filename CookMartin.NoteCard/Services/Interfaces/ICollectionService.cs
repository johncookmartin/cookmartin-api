using CookMartin.Data.Models.NoteCard.Collection;

namespace CookMartin.NoteCard.Services.Interfaces;

public interface ICollectionService
{
    Task<CollectionDto> CreateCollectionAsync(CreateCollectionDto dto);
    Task<CollectionDto?> GetCollectionByIdAsync(int collectionId);
    Task<IEnumerable<CollectionDto>> GetCollectionsByUserAsync(string userId);
    Task<int> CreateGuestCollectionAsync();
    Task<bool> UpdateCollectionAsync(int collectionId, UpdateCollectionDto dto);
    Task<bool> DeleteCollectionAsync(int collectionId);
}
