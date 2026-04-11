using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.RecurringItem;
using CookMartin.Data.SqlAccess.Budget.Interfaces;

namespace CookMartin.Budget.Services;

public class RecurringItemService : IRecurringItemService
{
    private readonly IRecurringItemRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public RecurringItemService(IRecurringItemRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<RecurringItemDto> CreateAsync(CreateRecurringItemDto dto)
    {
        var recurringItemId = await _transactionRunner.ExecuteAsync(async writeDb =>
            await _repository.CreateAsync(writeDb, dto));

        var item = await _repository.GetByIdAsync(recurringItemId);
        return item ?? throw new InvalidOperationException("Failed to create recurring item");
    }

    public async Task<RecurringItemDto?> GetByIdAsync(int recurringItemId)
    {
        return await _repository.GetByIdAsync(recurringItemId);
    }

    public async Task<IEnumerable<RecurringItemDto>> GetByCollectionAsync(int collectionId)
    {
        return await _repository.GetByCollectionAsync(collectionId);
    }

    public async Task<bool> UpdateAsync(UpdateRecurringItemDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });
    }

    public async Task<bool> DeleteAsync(int recurringItemId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, recurringItemId);
            return rowsAffected > 0;
        });
    }
}
