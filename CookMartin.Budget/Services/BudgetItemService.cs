using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.BudgetItem;
using CookMartin.Data.SqlAccess.Budget.Interfaces;

namespace CookMartin.Budget.Services;

public class BudgetItemService : IBudgetItemService
{
    private readonly IBudgetItemRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public BudgetItemService(IBudgetItemRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<BudgetItemDto> CreateAsync(CreateBudgetItemDto dto)
    {
        var budgetItemId = await _transactionRunner.ExecuteAsync(async writeDb =>
            await _repository.CreateAsync(writeDb, dto));

        var item = await _repository.GetByIdAsync(budgetItemId);
        return item ?? throw new InvalidOperationException("Failed to create budget item");
    }

    public async Task<BudgetItemDto?> GetByIdAsync(int budgetItemId)
    {
        return await _repository.GetByIdAsync(budgetItemId);
    }

    public async Task<IEnumerable<BudgetItemDto>> GetByBudgetAsync(int budgetId)
    {
        return await _repository.GetByBudgetAsync(budgetId);
    }

    public async Task<bool> UpdateAsync(UpdateBudgetItemDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });
    }

    public async Task<bool> DeleteAsync(int budgetItemId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, budgetItemId);
            return rowsAffected > 0;
        });
    }
}
