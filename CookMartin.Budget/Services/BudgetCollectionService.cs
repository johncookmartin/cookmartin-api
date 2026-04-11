using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Collection;
using CookMartin.Data.SqlAccess.Budget.Interfaces;

namespace CookMartin.Budget.Services;

public class BudgetCollectionService : IBudgetCollectionService
{
    private readonly IBudgetCollectionRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public BudgetCollectionService(IBudgetCollectionRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<BudgetCollectionDto> CreateAsync(CreateBudgetCollectionDto dto)
    {
        var collectionId = await _transactionRunner.ExecuteAsync(async writeDb =>
            await _repository.CreateAsync(writeDb, dto));

        var collection = await _repository.GetByIdAsync(collectionId);
        return collection ?? throw new InvalidOperationException("Failed to create collection");
    }

    public async Task<BudgetCollectionDto?> GetByIdAsync(int collectionId)
    {
        return await _repository.GetByIdAsync(collectionId);
    }

    public async Task<IEnumerable<BudgetCollectionDto>> GetByUserAsync(string userId)
    {
        return await _repository.GetByUserAsync(userId);
    }

    public async Task<bool> UpdateAsync(UpdateBudgetCollectionDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });
    }

    public async Task<bool> DeleteAsync(int collectionId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, collectionId);
            return rowsAffected > 0;
        });
    }

    public async Task<int> AddUserAsync(int collectionId, string userId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
            await _repository.AddUserAsync(writeDb, collectionId, userId));
    }

    public async Task<bool> RemoveUserAsync(int collectionId, string userId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.RemoveUserAsync(writeDb, collectionId, userId);
            return rowsAffected > 0;
        });
    }
}
