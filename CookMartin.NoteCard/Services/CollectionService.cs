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
        return await _repository.GetByUserAsync(userId);
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
