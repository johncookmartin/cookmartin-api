using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Budget;

namespace CookMartin.Data.SqlAccess.Budget.Interfaces;

public interface IBudgetRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetDto dto);
    Task<BudgetDto?> GetByIdAsync(int budgetId);
    Task<IEnumerable<BudgetDto>> GetByCollectionAsync(int collectionId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int budgetId);
}
