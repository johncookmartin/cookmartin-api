using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Collection;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.NoteCard.Services.Interfaces;

namespace CookMartin.NoteCard.Services;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public CollectionService(ICollectionRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<CollectionDto> CreateCollectionAsync(CreateCollectionDto dto)
    {
        var collectionId = await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            return await _repository.CreateAsync(writeDb, dto);
        });
        var collection = await _repository.GetByIdAsync(collectionId);
        return collection ?? throw new InvalidOperationException("Failed to create collection");
    }

    public async Task<CollectionDto?> GetCollectionByIdAsync(int collectionId)
    {
        return await _repository.GetByIdAsync(collectionId);
    }

    public async Task<IEnumerable<CollectionDto>> GetCollectionsByUserAsync(string userId)
    {
        IEnumerable<CollectionDto> collections = await _repository.GetByUserAsync(userId);
        if (userId == "guest" && collections.Count() < 1)
        {
            int addedCards = await CreateGuestCollectionAsync();
            collections = await _repository.GetByUserAsync(userId);
        }

        return collections;
    }

    public async Task<int> CreateGuestCollectionAsync()
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            int addedCards = await _repository.CreateDefaultGuestCollectionAsync(writeDb);
            return addedCards < 1 ? throw new Exception("Failed to create default guest collection") : addedCards;
        });
    }

    public async Task<bool> UpdateCollectionAsync(int collectionId, UpdateCollectionDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });

    }

    public async Task<bool> DeleteCollectionAsync(int collectionId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, collectionId);
            return rowsAffected > 0;
        });
    }
}
