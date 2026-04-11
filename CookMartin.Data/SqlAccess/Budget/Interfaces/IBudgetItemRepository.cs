using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.BudgetItem;

namespace CookMartin.Data.SqlAccess.Budget.Interfaces;

public interface IBudgetItemRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetItemDto dto);
    Task<BudgetItemDto?> GetByIdAsync(int budgetItemId);
    Task<IEnumerable<BudgetItemDto>> GetByBudgetAsync(int budgetId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetItemDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int budgetItemId);
}
