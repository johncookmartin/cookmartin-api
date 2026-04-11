using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Budget;
using CookMartin.Data.SqlAccess.Budget.Interfaces;

namespace CookMartin.Budget.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public BudgetService(IBudgetRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<BudgetDto> CreateAsync(CreateBudgetDto dto)
    {
        var budgetId = await _transactionRunner.ExecuteAsync(async writeDb =>
            await _repository.CreateAsync(writeDb, dto));

        var budget = await _repository.GetByIdAsync(budgetId);
        return budget ?? throw new InvalidOperationException("Failed to create budget");
    }

    public async Task<BudgetDto?> GetByIdAsync(int budgetId)
    {
        return await _repository.GetByIdAsync(budgetId);
    }

    public async Task<IEnumerable<BudgetDto>> GetByCollectionAsync(int collectionId)
    {
        return await _repository.GetByCollectionAsync(collectionId);
    }

    public async Task<bool> UpdateAsync(UpdateBudgetDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });
    }

    public async Task<bool> DeleteAsync(int budgetId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, budgetId);
            return rowsAffected > 0;
        });
    }
}
