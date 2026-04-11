using CookMartin.Data.Models.Budget.Budget;

namespace CookMartin.Budget.Services.Interfaces;

public interface IBudgetService
{
    Task<BudgetDto> CreateAsync(CreateBudgetDto dto);
    Task<BudgetDto?> GetByIdAsync(int budgetId);
    Task<IEnumerable<BudgetDto>> GetByCollectionAsync(int collectionId);
    Task<bool> UpdateAsync(UpdateBudgetDto dto);
    Task<bool> DeleteAsync(int budgetId);
}
