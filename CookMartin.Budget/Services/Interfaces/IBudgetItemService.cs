using CookMartin.Data.Models.Budget.BudgetItem;

namespace CookMartin.Budget.Services.Interfaces;

public interface IBudgetItemService
{
    Task<BudgetItemDto> CreateAsync(CreateBudgetItemDto dto);
    Task<BudgetItemDto?> GetByIdAsync(int budgetItemId);
    Task<IEnumerable<BudgetItemDto>> GetByBudgetAsync(int budgetId);
    Task<bool> UpdateAsync(UpdateBudgetItemDto dto);
    Task<bool> DeleteAsync(int budgetItemId);
}
